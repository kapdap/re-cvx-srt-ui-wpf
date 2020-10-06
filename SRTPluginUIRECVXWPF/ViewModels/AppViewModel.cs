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

        private IGameMemoryRECVX _gameMemory;
        public IGameMemoryRECVX GameMemory
        {
            get => _gameMemory;
            private set => SetField(ref _gameMemory, value);
        }

        private InventoryModel _inventory;
        public InventoryModel Inventory
        {
            get => _inventory;
            private set => SetField(ref _inventory, value);
        }

        private InventoryItem _equipment;
        public InventoryItem Equipment
        {
            get => _equipment;
            private set => SetField(ref _equipment, value);
        }

        public void Initalize(IGameMemoryRECVX gameMemory)
        {
            if (GameMemory != null)
                return;

            GameMemory = gameMemory;

            Inventory = new InventoryModel(GameMemory.Player.Inventory);
            Equipment = new InventoryItem(GameMemory.Player.Equipment);
        }
    }
}