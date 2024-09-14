using InventorySystem.Inventory.Core;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemAddedEventData
    {
        public readonly InventoryItem _item;

        public ItemAddedEventData(InventoryItem item)
        {
            _item = item;
        }
    }
}