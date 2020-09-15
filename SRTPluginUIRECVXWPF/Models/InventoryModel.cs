using SRTPluginProviderRECVX.Models;
using System.Collections.Generic;

namespace SRTPluginUIRECVXWPF.Models
{
    public class InventoryModel : BaseNotifyModel
    {
        public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();

        public InventoryModel(InventoryEntry[] entries) =>
            Initalize(entries);

        public void Initalize(InventoryEntry[] entries)
        {
            if (entries == null)
                return;

            for (int i = 0; i < entries.Length; i++)
                Items.Add(new InventoryItem(entries[i]));
        }
    }
}