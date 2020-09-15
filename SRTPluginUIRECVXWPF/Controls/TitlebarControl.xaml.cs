using System;
using System.Windows;
using System.Windows.Controls;

namespace SRTPluginUIRECVXWPF.Controls
{
    /// <summary>
    /// Interaction logic for TitlebarControl.xaml
    /// https://engy.us/blog/2020/01/01/implementing-a-custom-window-title-bar-in-wpf/
    /// </summary>
    public partial class TitlebarControl : UserControl
    {
        private Window _hostWindow;
        public Window HostWindow
        {
            get => _hostWindow != null ? _hostWindow : Window.GetWindow(this);
            private set => _hostWindow = value;
        }

        public bool ShowMinimizeButton { get; set; } = true;
        public bool ShowMaximizeButton { get; set; } = true;
        public bool ShowCloseButton { get; set; } = true;

        public TitlebarControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e) =>
            CloseWindow();

        private void MinimizeWindowButton_Click(object sender, RoutedEventArgs e) =>
            MinimizeWindow();

        private void MaximizeWindowButton_Click(object sender, RoutedEventArgs e) =>
            MaximizeRestoreWindow();

        private void RestoreWindowButton_Click(object sender, RoutedEventArgs e) =>
            MaximizeRestoreWindow();

        private void MinimizeWindow() =>
            HostWindow.WindowState = WindowState.Minimized;

        private void MaximizeRestoreWindow()
        {
            if (HostWindow.WindowState == WindowState.Maximized)
                HostWindow.WindowState = WindowState.Normal;
            else
                HostWindow.WindowState = WindowState.Maximized;
        }

        private void CloseWindow() =>
            HostWindow.Close();

        private void RefreshMaximizeRestoreButton()
        {
            if (HostWindow.WindowState == WindowState.Maximized)
            {
                MaximizeWindowButton.Visibility = Visibility.Collapsed;
                RestoreWindowButton.Visibility = Visibility.Visible;
            }
            else
            {
                MaximizeWindowButton.Visibility = Visibility.Visible;
                RestoreWindowButton.Visibility = Visibility.Collapsed;
            }
        }

        private void RefreshMaximizeRestoreButtonEvent(object sender, EventArgs e) =>
            RefreshMaximizeRestoreButton();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (_hostWindow == null && ShowMaximizeButton)
            {
                _hostWindow = Window.GetWindow(this);

                if (_hostWindow != null)
                {
                    _hostWindow.StateChanged += RefreshMaximizeRestoreButtonEvent;
                    RefreshMaximizeRestoreButton();
                }
            }
        }
    }
}