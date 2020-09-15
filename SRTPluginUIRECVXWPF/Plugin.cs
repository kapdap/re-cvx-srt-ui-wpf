using SRTPluginUIRECVXWPF.ViewModels;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;

namespace SRTPluginUIRECVXWPF
{
    public static class Plugin
    {
        public static bool IsExiting { get; private set; }

        public static PluginUI PluginUI { get; private set; }
        public static Thread UIThread { get; private set; }

        public static readonly string Version = String.Format("v{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());

        public static void Initialize(PluginUI plugin)
        {
            PluginUI = plugin;

            // https://stackoverflow.com/a/36006943
            ThreadStart ts = new ThreadStart(() =>
            {
                ActivateWindow();
                Dispatcher.Run();
            });

            UIThread = new Thread(ts);
            UIThread.SetApartmentState(ApartmentState.STA);
            UIThread.IsBackground = true;
            UIThread.Start();
        }

        public static void ActivateWindow() =>
            Windows.Main.Show();

        public static void Exit()
        {
            IsExiting = true;
            Windows.CloseAll();
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
                Options.Close();
            }
        }

        public static class Models
        {
            public static AppViewModel AppView { get; } = new AppViewModel();
        }
    }
}