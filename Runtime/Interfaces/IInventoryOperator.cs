using System;
using InventorySystem.Inventory.Core;
using InventorySystem.Operators.Base;

namespace InventorySystem.Interfaces
{
    public interface IInventoryOperator
    {
        void AddItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity);
        void RemoveItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity);
        void HandleRemoveItemById(Inventory.Core.Inventory inventory, InventoryItem item, int quantity);
    }
}