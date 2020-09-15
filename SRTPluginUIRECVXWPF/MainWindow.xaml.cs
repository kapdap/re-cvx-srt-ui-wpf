using System.ComponentModel;
using System.Windows;

namespace SRTPluginUIRECVXWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Plugin.Models.AppView;
        }

        private void OptionsMenuItem_Click(object sender, RoutedEventArgs e) => 
            Plugin.Windows.Options.Show();

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MinimizeMenuItem_Click(object sender, RoutedEventArgs e) =>
            WindowState = WindowState.Minimized;

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Plugin.IsExiting) return;
            Plugin.Exit();
        }
    }
}