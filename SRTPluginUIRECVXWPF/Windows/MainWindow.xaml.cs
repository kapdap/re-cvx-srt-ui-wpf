using SRTPluginProviderRECVX;
using SRTPluginUIRECVXWPF.Utilities;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace SRTPluginUIRECVXWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGameMemoryRECVX _gameMemory;
        private PluginConfig _options;

        private IntPtr _windowEventHook;
        private GCHandle _windowEventGCHandle;
        private bool _isAttachWindowUpdate;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = Plugin.Models.AppView;
            _gameMemory = Plugin.Models.AppView.GameMemory;
            _options = Plugin.Models.AppView.Options;

            ToggleAttachWindow(true);

            _options.PropertyChanged += Options_PropertyChanged;
            _gameMemory.Emulator.PropertyChanged += Emulator_PropertyChanged;
        }

        private void Options_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AttachToWindow")
                ToggleAttachWindow();
        }

        private void Emulator_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "GameWindowHandle")
                Plugin.DispatcherUI.Invoke(delegate
                {
                    ToggleAttachWindow(true);
                });
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

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (!_isAttachWindowUpdate)
                UpdateAttachWindowOffset();

            Properties.Settings.Default.Save();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try { _options.PropertyChanged -= Options_PropertyChanged; }
            catch (Exception) { }

            try { _gameMemory.Emulator.PropertyChanged -= Emulator_PropertyChanged; }
            catch (Exception) { }

            DisableAttactWindow();

            if (Plugin.IsExiting) return;
            Plugin.Exit();
        }

        protected void UpdateAttachWindowPosition()
        {
            if (_gameMemory.Emulator.GameWindowHandle == IntPtr.Zero)
                return;

            Utilities.Rect rect = WinEventHook.GetWindowRect(_gameMemory.Emulator.GameWindowHandle);

            _isAttachWindowUpdate = true;
            Top = rect.Top - Properties.Settings.Default.YOffset;
            Left = rect.Left - Properties.Settings.Default.XOffset;
            _isAttachWindowUpdate = false;
        }

        protected void UpdateAttachWindowOffset()
        {
            if (_gameMemory.Emulator.GameWindowHandle == IntPtr.Zero)
                return;

            Utilities.Rect rect = WinEventHook.GetWindowRect(_gameMemory.Emulator.GameWindowHandle);

            Properties.Settings.Default.YOffset = (int)(rect.Top - Top);
            Properties.Settings.Default.XOffset = (int)(rect.Left - Left);
        }

        protected void ToggleAttachWindow(bool isInit = false)
        {
            if (_options.AttachToWindow)
            {
                if (!isInit)
                    UpdateAttachWindowOffset();

                UpdateAttachWindowPosition();

                EnableAttactWindow();
            }
            else
                DisableAttactWindow();
        }

        protected void EnableAttactWindow()
        {
            if (_windowEventHook != IntPtr.Zero)
                return;

            try
            {
                WinEventHook.WinEventDelegate windowEventDelegate = new WinEventHook.WinEventDelegate(AttachWindowEventCallback);
                _windowEventGCHandle = GCHandle.Alloc(windowEventDelegate);
                _windowEventHook = WinEventHook.WinEventHookOne(WinEventHook.SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE,
                                                        windowEventDelegate,
                                                        (uint)_gameMemory.Emulator.Id,
                                                        WinEventHook.GetWindowThread(_gameMemory.Emulator.GameWindowHandle));
            }
            catch (Exception ex)
            {
                Plugin.ShowExceptionMessage(ex);
            }

            if (_windowEventHook == IntPtr.Zero)
                DisableAttactWindow();
        }

        protected void DisableAttactWindow()
        {
            try
            {
                if (_windowEventGCHandle.IsAllocated)
                    _windowEventGCHandle.Free();

                if (_windowEventHook != IntPtr.Zero)
                    WinEventHook.WinEventUnhook(_windowEventHook);
            }
            catch (Exception ex)
            {
                Plugin.ShowExceptionMessage(ex);
            }
            finally
            {
                _windowEventHook = IntPtr.Zero;
            }
        }

        protected void AttachWindowEventCallback(IntPtr hWinEventHook,
                           WinEventHook.SWEH_Events eventType,
                           IntPtr hWnd,
                           WinEventHook.SWEH_ObjectId idObject,
                           long idChild,
                           uint dwEventThread,
                           uint dwmsEventTime)
        {
            if (hWnd == _gameMemory.Emulator.GameWindowHandle &&
                eventType == WinEventHook.SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE &&
                idObject == WinEventHook.SWEH_ObjectId.OBJID_WINDOW)
                UpdateAttachWindowPosition();
        }
    }
}