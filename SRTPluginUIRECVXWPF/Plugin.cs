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
            Config.PropertyChanged += Config_PropertyChanged;

            try
            {
                // https://stackoverflow.com/a/36006943
                Thread t = new Thread(new ThreadStart(() =>
                {
                    DispatcherUI = Dispatcher.CurrentDispatcher;
                    DispatcherUI.Invoke(delegate
                    {
                        Windows.Main.AllowsTransparency = Config.Transparent;
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

            if (Config != null)
            {
                PluginUI.SaveConfiguration(Config);

                try { Config.PropertyChanged -= Config_PropertyChanged; }
                catch (Exception) { }
            }

            Properties.Settings.Default.Save();

            try
            {
                if (DispatcherUI != null)
                {
                    DispatcherUI.Invoke(delegate
                    {
                        Models.Dispose();
                        Windows.Close();
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

        private static void Config_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Config.Transparent))
                DispatcherUI.Invoke(delegate
                {
                    Windows.ReopenMain();
                });

            PluginUI.SaveConfiguration(Config);
        }

        public static class Windows
        {
            private static MainWindow _main;
            public static MainWindow Main
            {
                get => _main ??= new MainWindow();
                set => _main = value;
            }

            private static OptionsWindow _options;
            public static OptionsWindow Options
            {
                get => _options ??= new OptionsWindow();
                set => _options = value;
            }

            private static AboutWindow _about;
            public static AboutWindow About
            {
                get => _about ??= new AboutWindow();
                set => _about = value;
            }

            public static void Close()
            {
                Close(_about);
                Close(_options);
                Close(_main);

                _about = null;
                _options = null;
                _main = null;
            }

            public static void Close(Window window)
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

            public static void ReopenMain()
            {
                Close(_main);
                _main = null;

                Main.AllowsTransparency = Config.Transparent;
                Main.Show();
            }
        }

        public static class Models
        {
            private static AppViewModel _appView;
            public static AppViewModel AppView
            {
                get => _appView ??= new AppViewModel();
                set => _appView = value;
            }

            public static void Dispose()
            {
                _appView?.Dispose();
                _appView = null;
            }
        }
    }
}