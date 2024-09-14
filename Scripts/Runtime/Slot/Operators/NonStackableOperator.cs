using System;
using InventorySystem.Factories;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class NonStackableOperator : IInventoryOperator
    {
        public void AddItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                InventoryItem newItem = InventoryItemFactory.CreateItem(item.Data, 1);
                inventory.QueryHandler.TryAddToInventory(newItem, triggerEvent: true);
            }
        }

        public void RemoveItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            inventory.QueryHandler.TryRemoveFromInventory(item, triggerEvent: true);
        }

        public void HandleRemoveItemById(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            RemoveItem(inventory, item, quantity);
        }
    }
}