using SRTPluginBase;
using SRTPluginProviderRECVX;
using SRTPluginProviderRECVX.Models;
using SRTPluginUIRECVXWPF.Models;
using System;

namespace SRTPluginUIRECVXWPF.ViewModels
{
    public class AppViewModel : BaseNotifyModel
    {
        public IPluginInfo PluginInfo => Plugin.PluginUI.Info;

        public string PluginName => Plugin.Name;
        public string PluginVersion => Plugin.Version;
        public string PluginProvider => Plugin.PluginUI.RequiredProvider;
        public string PluginTitle => String.Format("RE CVX SRT {0}", Plugin.Version);

        public OptionModel Options { get; } = new OptionModel();

        private GameMemoryRECVX _gameMemory;
        public GameMemoryRECVX GameMemory
        {
            get => _gameMemory;
            private set
            {
                _gameMemory = value;
                OnPropertyChanged();
            }
        }

        private InventoryModel _inventory;
        public InventoryModel Inventory
        {
            get => _inventory;
            private set
            {
                _inventory = value;
                OnPropertyChanged();
            }
        }

        private InventoryItem _equipment;
        public InventoryItem Equipment
        {
            get => _equipment;
            private set
            {
                _equipment = value;
                OnPropertyChanged();
            }
        }

        public void Initalize(GameMemoryRECVX gameMemory)
        {
            if (GameMemory != null)
                return;

            GameMemory = gameMemory;

            Inventory = new InventoryModel(GameMemory.Player.Inventory);
            Equipment = new InventoryItem(GameMemory.Player.Equipment);
        }
    }
}