using SRTPluginBase;
using SRTPluginProviderRECVX;
using SRTPluginProviderRECVX.Models;
using SRTPluginUIRECVXWPF.Models;
using System;
using System.ComponentModel;
using System.Windows.Controls.Primitives;

namespace SRTPluginUIRECVXWPF.ViewModels
{
    public class AppViewModel : BaseNotifyModel, IDisposable
    {
        public IPluginInfo PluginInfo => Plugin.PluginUI.Info;

        public string PluginName => Plugin.Name;
        public string PluginVersion => Plugin.Version;
        public string PluginProvider => Plugin.PluginUI.RequiredProvider;
        public string PluginTitle => String.Format("RE CVX SRT {0}", Plugin.Version);

        public PluginConfig Options { get; } = Plugin.Config;

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

            ToggleFindGameWindowHandler();
            Options.PropertyChanged += ChangedConfiguration;
        }

        private void ChangedConfiguration(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AttachToWindow")
                ToggleFindGameWindowHandler();
        }

        private void ToggleFindGameWindowHandler() =>
            GameMemory.Emulator.DetectGameWindowHandle = Options.AttachToWindow;

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    try { Options.PropertyChanged -= ChangedConfiguration; }
                    catch (Exception) { }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}