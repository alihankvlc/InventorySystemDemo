using InventorySystem.Inventory.Core;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemStackedEventData
    {
        public readonly InventoryItem _item;

        public ItemStackedEventData(InventoryItem item)
        {
            _item = item;
        }
    }
}