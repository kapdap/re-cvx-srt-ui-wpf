using SRTPluginUIRECVXWPF.ViewModels;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SRTPluginUIRECVXWPF
{
    public static class Plugin
    {
        public static bool IsExiting { get; private set; }

        public static PluginConfig Config { get; private set; }
        public static PluginUI PluginUI { get; private set; }
        public static Dispatcher DispatcherUI { get; private set; }

        public static readonly string Name = Assembly.GetExecutingAssembly().GetName().Name.ToString();
        public static readonly string Version = String.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

        public static int Initialize(PluginUI plugin)
        {
            PluginUI = plugin;

            Config = PluginUI.LoadConfiguration<PluginConfig>();
            Config.PropertyChanged += ChangedConfiguration;

            try
            {
                // https://stackoverflow.com/a/36006943
                Thread t = new Thread(new ThreadStart(() =>
                {
                    DispatcherUI = Dispatcher.CurrentDispatcher;
                    DispatcherUI.Invoke(delegate
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

            PluginUI.SaveConfiguration(Config);
            Properties.Settings.Default.Save();

            try { Config.PropertyChanged -= ChangedConfiguration; }
            catch (Exception) { }

            try
            {
                if (DispatcherUI != null)
                {
                    DispatcherUI.Invoke(delegate
                    {
                        Windows.CloseAll();
                        Models.DisposeAll();
                    });
                    DispatcherUI.InvokeShutdown();
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
                Config = null;
                DispatcherUI = null;
                IsExiting = false;
            }
        }

        public static void ShowMessage(string message) =>
            PluginUI.HostDelegates.OutputMessage.Invoke(message);

        public static void ShowExceptionMessage(Exception ex) =>
            PluginUI.HostDelegates.ExceptionMessage.Invoke(ex);

        private static void ChangedConfiguration(object sender, PropertyChangedEventArgs e) =>
            PluginUI.SaveConfiguration(Config);

        public static class Windows
        {
            private static MainWindow _main;
            public static MainWindow Main
            {
                get => _main == null ? _main = new MainWindow() : _main;
                set => _main = value;
            }

            private static OptionsWindow _options;
            public static OptionsWindow Options
            {
                get => _options == null ? _options = new OptionsWindow() : _options;
                set => _options = value;
            }

            private static AboutWindow _about;
            public static AboutWindow About
            {
                get => _about == null ? _about = new AboutWindow() : _about;
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
                get => _appView == null ? _appView = new AppViewModel() : _appView;
                set => _appView = value;
            }

            public static void DisposeAll()
            {
                _appView?.Dispose();
                _appView = null;
            }
        }
    }
}