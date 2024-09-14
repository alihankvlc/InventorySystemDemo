using InventorySystem.Inventory.Core;
using InventorySystem.Utility;

namespace InventorySystem.Factories
{
    public static class InventoryItemFactory
    {
        public static InventoryItem CreateItem(IItem item, int quantity)
        {
            return new(item, quantity, IDPool.TakeID());
        }
    }
}