using SRTPluginUIRECVXWPF.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SRTPluginUIRECVXWPF
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            DataContext = Plugin.Models.AppView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Plugin.IsExiting) return;

            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void UserInterfaceLink_MouseUp(object sender, MouseButtonEventArgs e) =>
            URLHelper.OpenURL("https://github.com/kapdap/re-cvx-srt-ui-wpf");

        private void MemoryProviderLink_MouseUp(object sender, MouseButtonEventArgs e) =>
            URLHelper.OpenURL("https://github.com/kapdap/re-cvx-srt-provider");

        private void PluginHostLink_MouseUp(object sender, MouseButtonEventArgs e) =>
            URLHelper.OpenURL("https://www.neonblu.com/SRT/");

        private void WebsiteLink_MouseUp(object sender, MouseButtonEventArgs e) =>
            URLHelper.OpenURL("https://kapdap.github.io/re-cvx-srt/");

        private void GitHubLink_MouseUp(object sender, MouseButtonEventArgs e) =>
            URLHelper.OpenURL("https://github.com/kapdap/re-cvx-srt-ui-wpf");

        private void LicenseLink_MouseUp(object sender, MouseButtonEventArgs e) =>
            URLHelper.OpenURL("https://github.com/kapdap/re-cvx-srt-ui-wpf/blob/master/LICENSE");
    }
}
