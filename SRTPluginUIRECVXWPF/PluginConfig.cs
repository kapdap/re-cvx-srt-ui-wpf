using SRTPluginProviderRECVX.Models;
using System;
using System.Windows.Media;

namespace SRTPluginUIRECVXWPF
{
    public class PluginConfig : BaseNotifyModel
    {
        private bool _alwaysOnTop = false;
        public bool AlwaysOnTop
        {
            get => _alwaysOnTop;
            set => SetField(ref _alwaysOnTop, value);
        }

        private bool _attachToWindow = false;
        public bool AttachToWindow
        {
            get => _attachToWindow;
            set => SetField(ref _attachToWindow, value);
        }

        private Color _background = Color.FromArgb(255, 0, 0, 0);
        public Color Background
        {
            get => _background;
            set => SetField(ref _background, value);
        }

        private bool _transparent = false;
        public bool Transparent
        {
            get => _transparent;
            set => SetField(ref _transparent, value);
        }

        private double _opacity = 1d;
        public double Opacity
        {
            get => _opacity;
            set => SetField(ref _opacity, GetRange(value, 0.05, 1d, 2));
        }

        private double _statusScale = 1d;
        public double StatusScale
        {
            get => _statusScale;
            set => SetField(ref _statusScale, GetRange(value, 0.5, 2d, 2));
        }

        private double _statisticsScale = 1d;
        public double StatisticsScale
        {
            get => _statisticsScale;
            set => SetField(ref _statisticsScale, GetRange(value, 0.5, 2d, 2));
        }

        private double _enemyScale = 1d;
        public double EnemyScale
        {
            get => _enemyScale;
            set => SetField(ref _enemyScale, GetRange(value, 0.5, 2d, 2));
        }

        private double _inventoryScale = 1d;
        public double InventoryScale
        {
            get => _inventoryScale;
            set => SetField(ref _inventoryScale, GetRange(value, 0.5, 2d, 2));
        }

        private bool _showTitlebar = false;
        public bool ShowTitlebar
        {
            get => _showTitlebar;
            set => SetField(ref _showTitlebar, value);
        }

        private bool _showTimer = true;
        public bool ShowTimer
        {
            get => _showTimer;
            set => SetField(ref _showTimer, value);
        }

        private bool _showStatistics = true;
        public bool ShowStatistics
        {
            get => _showStatistics;
            set => SetField(ref _showStatistics, value);
        }

        private bool _showRanking = false;
        public bool ShowRanking
        {
            get => _showRanking;
            set => SetField(ref _showRanking, value);
        }

        private bool _showEnemy = true;
        public bool ShowEnemy
        {
            get => _showEnemy;
            set => SetField(ref _showEnemy, value);
        }

        private bool _showBosses = false;
        public bool ShowBosses
        {
            get => _showBosses;
            set => SetField(ref _showBosses, value);
        }

        private bool _showInventory = true;
        public bool ShowInventory
        {
            get => _showInventory;
            set => SetField(ref _showInventory, value);
        }

        private bool _showEquipment = false;
        public bool ShowEquipment
        {
            get => _showEquipment;
            set => SetField(ref _showEquipment, value);
        }

        private bool _swapInventory = false;
        public bool SwapInventory
        {
            get => _swapInventory;
            set => SetField(ref _swapInventory, value);
        }

        private bool _debug = false;
        public bool Debug
        {
            get => _debug;
            set => SetField(ref _debug, value);
        }

        private bool _debugEnemy = false;
        public bool DebugEnemy
        {
            get => _debugEnemy;
            set => SetField(ref _debugEnemy, value);
        }

        private double GetRange(double value, double min, double max, int? round = null)
        {
            value = Math.Max((double)value, min);
            value = Math.Min((double)value, max);

            if (round != null)
                value = Math.Round((double)value, (int)round);

            return value;
        }
    }
}