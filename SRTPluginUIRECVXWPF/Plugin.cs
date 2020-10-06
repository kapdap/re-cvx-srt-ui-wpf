using SRTPluginUIRECVXWPF.ViewModels;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SRTPluginUIRECVXWPF
{
    public static class Plugin
    {
        public static bool IsExiting { get; private set; }

        public static PluginUI PluginUI { get; private set; }
        public static Dispatcher UIDispatcher { get; private set; }

        public static readonly string Name = Assembly.GetExecutingAssembly().GetName().Name.ToString();
        public static readonly string Version = String.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

        public static int Initialize(PluginUI plugin)
        {
            PluginUI = plugin;

            try
            {
                // https://stackoverflow.com/a/36006943
                Thread t = new Thread(new ThreadStart(() =>
                {
                    UIDispatcher = Dispatcher.CurrentDispatcher;
                    UIDispatcher.Invoke(delegate
                    {
                        Windows.Main.Show();
                    });
                    Dispatcher.Run();
                }));

                t.SetApartmentState(ApartmentState.STA);
                t.IsBackground = true;
                t.Start();

                return 0;
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return 1;
            }
        }

        public static int Exit()
        {
            IsExiting = true;

            Properties.Settings.Default.Save();

            try
            {
                if (UIDispatcher != null)
                {
                    UIDispatcher.Invoke(delegate
                    {
                        Windows.CloseAll();
                        Models.DisposeAll();
                    });
                    UIDispatcher.InvokeShutdown();
                }

                return 0;
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return 1;
            }
            finally
            {
                UIDispatcher = null;
                IsExiting = false;
            }
        }

        public static void ShowMessage(string message) =>
            PluginUI.HostDelegates.OutputMessage(message);

        public static void ShowExceptionMessage(Exception ex) =>
            PluginUI.HostDelegates.ExceptionMessage(ex);

        public static class Windows
        {
            private static MainWindow _main;
            public static MainWindow Main
            {
                get
                {
                    if (_main == null)
                        _main = new MainWindow();
                    return _main;
                }
                set => _main = value;
            }

            private static OptionsWindow _options;
            public static OptionsWindow Options
            {
                get
                {
                    if (_options == null)
                        _options = new OptionsWindow();
                    return _options;
                }
                set => _options = value;
            }

            private static AboutWindow _about;
            public static AboutWindow About
            {
                get
                {
                    if (_about == null)
                        _about = new AboutWindow();
                    return _about;
                }
                set => _about = value;
            }

            public static void CloseAll()
            {
                CloseWindow(_about);
                CloseWindow(_options);
                CloseWindow(_main);

                _about = null;
                _options = null;
                _main = null;
            }

            public static void CloseWindow(Window window)
            {
                if (window == null)
                    return;

                try
                {
                    window.Close();
                }
                catch (InvalidOperationException) { }
                catch (Exception ex)
                {
                    ShowExceptionMessage(ex);
                }
            }
        }

        public static class Models
        {
            private static AppViewModel _appView;
            public static AppViewModel AppView
            {
                get
                {
                    if (_appView == null)
                        _appView = new AppViewModel();
                    return _appView;
                }
                set => _appView = value;
            }

            public static void DisposeAll()
            {
                _appView = null;
            }
        }
    }
}