using SRTPluginProviderRECVX;
using SRTPluginProviderRECVX.Models;
using SRTPluginUIRECVXWPF.Models;
using System;

namespace SRTPluginUIRECVXWPF.ViewModels
{
    public class AppViewModel : BaseNotifyModel
    {
        public string PluginVersion => Plugin.Version;
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

        public void Initalize(GameMemoryRECVX gameMemory)
        {
            if (GameMemory != null)
                return;

            GameMemory = gameMemory;
            Inventory = new InventoryModel(GameMemory.Player.Inventory);
        }
    }
}