using InventorySystem.Inventory.Core;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemMergeEventData
    {
        public readonly InventoryItem _fromItem;
        public readonly InventoryItem _toItem;

        public ItemMergeEventData(InventoryItem fromItem, InventoryItem toItem)
        {
            _fromItem = fromItem;
            _toItem = toItem;
        }
    }
}