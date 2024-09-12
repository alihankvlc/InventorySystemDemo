using System;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class StackableOperator : IInventoryOperator
    {
        public void AddItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            if (inventory.TryFindStackableItem(item.Data.ID, out InventoryItem existingItem))
            {
                existingItem.AddQuantity(quantity);
                InventoryNotifier.OnItemStacked?.Invoke(existingItem);
                return;
            }

            inventory.TryAddToInventory(item);
            InventoryNotifier.OnItemAdded?.Invoke(item);
        }

        public void RemoveItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity, Action callback = null)
        {
            if (item != null)
            {
                item.RemoveQuantity(quantity);
                InventoryNotifier.OnItemStacked?.Invoke(item);
            
                if (item.Quantity <= 0)
                {
                    callback?.Invoke();
                    InventoryNotifier.OnItemRemoved?.Invoke(item);
                }
            }
        }
    }
}