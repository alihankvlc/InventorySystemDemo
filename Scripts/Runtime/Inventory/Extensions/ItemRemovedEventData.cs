using InventorySystem.Inventory.Core;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemRemovedEventData
    {
        public readonly InventoryItem _item;

        public ItemRemovedEventData(InventoryItem item)
        {
            _item = item;
        }
    }
}