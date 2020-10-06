using System.Windows.Data;

// https://thomaslevesque.com/2008/11/18/wpf-binding-to-application-settings-using-a-markup-extension/
namespace SRTPluginUIRECVXWPF.Utilities
{
    public class SettingBinding : Binding
    {
        public SettingBinding() =>
            Initialize();

        public SettingBinding(string path) : base(path) =>
            Initialize();

        private void Initialize()
        {
            Source = Properties.Settings.Default;
            Mode = BindingMode.TwoWay;
        }
    }
}