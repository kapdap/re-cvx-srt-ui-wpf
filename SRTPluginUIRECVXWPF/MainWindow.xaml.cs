using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

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

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e) =>
            Plugin.Windows.About.Show();

        private void MinimizeMenuItem_Click(object sender, RoutedEventArgs e) =>
            WindowState = WindowState.Minimized;

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e) =>
            Close();

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Plugin.IsExiting) return;
            Plugin.Exit();
        }
    }
}