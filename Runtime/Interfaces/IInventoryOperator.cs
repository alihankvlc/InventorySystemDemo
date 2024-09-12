using System;
using InventorySystem.Inventory.Core;

namespace InventorySystem.Interfaces
{
    public interface IInventoryOperator
    {
        void AddItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity);
        void RemoveItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity, Action callback = null);
    }
}