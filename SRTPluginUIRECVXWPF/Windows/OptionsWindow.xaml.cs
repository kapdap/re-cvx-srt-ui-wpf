using System.ComponentModel;
using System.Windows;

namespace SRTPluginUIRECVXWPF
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            DataContext = Plugin.Models.AppView;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Plugin.IsExiting) return;

            Visibility = Visibility.Hidden;
            e.Cancel = true;
        }
    }
}