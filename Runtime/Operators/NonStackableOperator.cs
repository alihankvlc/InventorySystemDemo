using System;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;

namespace InventorySystem.Operators
{
    public class NonStackableOperator : IInventoryOperator
    {
        public void AddItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            inventory.TryAddToInventory(item);
            InventoryNotifier.OnItemAdded?.Invoke(item);
        }

        public void RemoveItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity, Action callback = null)
        {
            callback?.Invoke();
            InventoryNotifier.OnItemRemoved?.Invoke(item);
        }
    }
}