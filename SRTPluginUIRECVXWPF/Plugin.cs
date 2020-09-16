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

            try
            {
                UIDispatcher.InvokeShutdown();
                return 0;
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
                return 1;
            }
        }

        public static void ShowMessage(string message) =>
            PluginUI.HostDelegates.OutputMessage(message);

        public static void ShowExceptionMessage(Exception ex) =>
            PluginUI.HostDelegates.ExceptionMessage(ex);

        public static class Windows
        {
            public static MainWindow Main { get; } = new MainWindow();
            public static OptionsWindow Options { get; } = new OptionsWindow();

            public static void CloseAll()
            {
                CloseWindow(Options);
                CloseWindow(Main);
            }

            private static void CloseWindow(Window window)
            {
                try
                {
                    window.Close();
                }
                catch(Exception ex)
                {
                    ShowExceptionMessage(ex);
                }
            }
        }

        public static class Models
        {
            public static AppViewModel AppView { get; } = new AppViewModel();
        }
    }
}