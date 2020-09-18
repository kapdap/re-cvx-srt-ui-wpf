using Microsoft.Win32;
using SRTPluginProviderRECVX.Models;
using SRTPluginUIRECVXWPF.Utilities;
using System;
using System.Windows.Media;

namespace SRTPluginUIRECVXWPF.Models
{
    public class OptionModel : BaseNotifyModel
    {
        private RegistryKey _readKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\re-cvx-srt-ui-wpf", false);
        private RegistryKey _writeKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\re-cvx-srt-ui-wpf", true);

        private bool? _alwaysOnTop;
        public bool AlwaysOnTop
        {
            get
            {
                if (_alwaysOnTop == null)
                    _alwaysOnTop = RegistryHelper.GetBoolValue(_readKey, "AlwaysOnTop", false);
                return (bool)_alwaysOnTop;
            }
            set
            {
                if (_alwaysOnTop != value)
                {
                    _alwaysOnTop = value;
                    _writeKey.SetValue("AlwaysOnTop", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showTitlebar;
        public bool ShowTitlebar
        {
            get
            {
                if (_showTitlebar == null)
                    _showTitlebar = RegistryHelper.GetBoolValue(_readKey, "ShowTitlebar", true);
                return (bool)_showTitlebar;
            }
            set
            {
                if (_showTitlebar != value)
                {
                    _showTitlebar = value;
                    _writeKey.SetValue("ShowTitlebar", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private Color? _background;
        public Color Background
        {
            get
            {
                if (_background == null)
                {
                    byte[] bytes = BitConverter.GetBytes(RegistryHelper.GetValue(_readKey, "Background", System.Drawing.Color.Black.ToArgb()));
                    _background = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
                }
                return (Color)_background;
            }
            set
            {
                if (_background != value)
                {
                    _background = value;
                    _writeKey.SetValue("Background", BitConverter.ToInt32(new byte[4]{value.B, value.G, value.R, value.A}), RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _transparent;
        public bool Transparent
        {
            get
            {
                if (_transparent == null)
                    _transparent = RegistryHelper.GetBoolValue(_readKey, "Transparent", true);
                return (bool)_transparent;
            }
            set
            {
                if (_transparent != value)
                {
                    _transparent = value;
                    _writeKey.SetValue("Transparent", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private double? _opacity;
        public double Opacity
        {
            get
            {
                if (_opacity == null)
                    _opacity = RegistryHelper.GetDoubleValue(_readKey, "Opacity", 1.0d);

                _opacity = Math.Max((double)_opacity, 0.05);
                _opacity = Math.Min((double)_opacity, 1.00);
                _opacity = Math.Round((double)_opacity, 2);

                return (double)_opacity;
            }
            set
            {
                if (_opacity != value)
                {
                    _opacity = Math.Max(value, 0.05);
                    _opacity = Math.Min((double)_opacity, 1.00);
                    _opacity = Math.Round((double)_opacity, 2);

                    _writeKey.SetValue("Opacity", _opacity, RegistryValueKind.String);
                    OnPropertyChanged();
                }
            }
        }

        private double? _textScale;
        public double TextScale
        {
            get
            {
                if (_textScale == null)
                    _textScale = RegistryHelper.GetDoubleValue(_readKey, "TextScale", 1.0d);

                _textScale = Math.Max((double)_textScale, 0.50);
                _textScale = Math.Min((double)_textScale, 1.00);
                _textScale = Math.Round((double)_textScale, 2);

                return (double)_textScale;
            }
            set
            {
                if (_textScale != value)
                {
                    _textScale = Math.Max(value, 0.50);
                    _textScale = Math.Min((double)_textScale, 1.00);
                    _textScale = Math.Round((double)_textScale, 2);

                    _writeKey.SetValue("TextScale", _textScale, RegistryValueKind.String);
                    OnPropertyChanged();
                }
            }
        }

        private double? _inventoryScale;
        public double InventoryScale
        {
            get
            {
                if (_inventoryScale == null)
                    _inventoryScale = RegistryHelper.GetDoubleValue(_readKey, "InventoryScale", 1.0d);

                _inventoryScale = Math.Max((double)_inventoryScale, 0.50);
                _inventoryScale = Math.Min((double)_inventoryScale, 1.00);
                _inventoryScale = Math.Round((double)_inventoryScale, 2);

                return (double)_inventoryScale;
            }
            set
            {
                if (_inventoryScale != value)
                {
                    _inventoryScale = Math.Max(value, 0.50);
                    _inventoryScale = Math.Min((double)_inventoryScale, 1.00);
                    _inventoryScale = Math.Round((double)_inventoryScale, 2);

                    _writeKey.SetValue("InventoryScale", _inventoryScale, RegistryValueKind.String);
                    OnPropertyChanged();
                }
            }
        }

        private double? _statusScale;
        public double StatusScale
        {
            get
            {
                if (_statusScale == null)
                    _statusScale = RegistryHelper.GetDoubleValue(_readKey, "StatusScale", 1.0d);

                _statusScale = Math.Max((double)_statusScale, 0.50);
                _statusScale = Math.Min((double)_statusScale, 1.00);
                _statusScale = Math.Round((double)_statusScale, 2);

                return (double)_statusScale;
            }
            set
            {
                if (_statusScale != value)
                {
                    _statusScale = Math.Max(value, 0.50);
                    _statusScale = Math.Min((double)_statusScale, 1.00);
                    _statusScale = Math.Round((double)_statusScale, 2);

                    _writeKey.SetValue("StatusScale", _statusScale, RegistryValueKind.String);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showTimer;
        public bool ShowTimer
        {
            get
            {
                if (_showTimer == null)
                    _showTimer = RegistryHelper.GetBoolValue(_readKey, "ShowTimer", true);
                return (bool)_showTimer;
            }
            set
            {
                if (_showTimer != value)
                {
                    _showTimer = value;
                    _writeKey.SetValue("ShowTimer", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showStatistics;
        public bool ShowStatistics
        {
            get
            {
                if (_showStatistics == null)
                    _showStatistics = RegistryHelper.GetBoolValue(_readKey, "ShowStatistics", true);
                return (bool)_showStatistics;
            }
            set
            {
                if (_showStatistics != value)
                {
                    _showStatistics = value;
                    _writeKey.SetValue("ShowStatistics", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showInventory;
        public bool ShowInventory
        {
            get
            {
                if (_showInventory == null)
                    _showInventory = RegistryHelper.GetBoolValue(_readKey, "ShowInventory", true);
                return (bool)_showInventory;
            }
            set
            {
                if (_showInventory != value)
                {
                    _showInventory = value;
                    _writeKey.SetValue("ShowInventory", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showEquipment;
        public bool ShowEquipment
        {
            get
            {
                if (_showEquipment == null)
                    _showEquipment = RegistryHelper.GetBoolValue(_readKey, "ShowEquipment", false);
                return (bool)_showEquipment;
            }
            set
            {
                if (_showEquipment != value)
                {
                    _showEquipment = value;
                    _writeKey.SetValue("ShowEquipment", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showEnemy;
        public bool ShowEnemy
        {
            get
            {
                if (_showEnemy == null)
                    _showEnemy = RegistryHelper.GetBoolValue(_readKey, "ShowEnemy", true);
                return (bool)_showEnemy;
            }
            set
            {
                if (_showEnemy != value)
                {
                    _showEnemy = value;
                    _writeKey.SetValue("ShowEnemy", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _showBosses;
        public bool ShowBosses
        {
            get
            {
                if (_showBosses == null)
                    _showBosses = RegistryHelper.GetBoolValue(_readKey, "ShowBosses", false);
                return (bool)_showBosses;
            }
            set
            {
                if (_showBosses != value)
                {
                    _showBosses = value;
                    _writeKey.SetValue("ShowBosses", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _debug;
        public bool Debug
        {
            get
            {
                if (_debug == null)
                    _debug = RegistryHelper.GetBoolValue(_readKey, "Debug", false);
                return (bool)_debug;
            }
            set
            {
                if (_debug != value)
                {
                    _debug = value;
                    _writeKey.SetValue("Debug", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }

        private bool? _debugEnemy;
        public bool DebugEnemy
        {
            get
            {
                if (_debugEnemy == null)
                    _debugEnemy = RegistryHelper.GetBoolValue(_readKey, "DebugEnemy", false);
                return (bool)_debugEnemy;
            }
            set
            {
                if (_debugEnemy != value)
                {
                    _debugEnemy = value;
                    _writeKey.SetValue("DebugEnemy", value ? 1 : 0, RegistryValueKind.DWord);
                    OnPropertyChanged();
                }
            }
        }
    }
}