using System;
using System.Linq;
using InventorySystem.Factories;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Utility;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class StackableOperator : IInventoryOperator
    {
        public void AddItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            while (quantity > 0)
            {
                InventoryItem existingStackableItem = inventory.QueryHandler.FindExistingStackableItem(item.Data.ID);
                int maxStackSize = (item.Data as IStackable).MaxStackSize;

                if (existingStackableItem != null)
                {
                    int addQuantity = Mathf.Min(quantity, maxStackSize - existingStackableItem.Quantity);
                    existingStackableItem.AddQuantity(addQuantity);
                    EventBus.Publish(new ItemStackedEventData(existingStackableItem));

                    quantity -= addQuantity;
                }

                if (quantity > 0)
                {
                    int newStackQuantity = Mathf.Min(quantity, maxStackSize);
                    InventoryItem newItem = InventoryItemFactory.CreateItem(item.Data, newStackQuantity);
                    inventory.QueryHandler.TryAddToInventory(newItem, false);
                    EventBus.Publish(new ItemAddedEventData(newItem));
                    quantity -= newStackQuantity;
                }
            }
        }

        public void RemoveItem(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            while (quantity > 0)
            {
                InventoryItem existingStackableItem =
                    inventory.QueryHandler.FindItemByDataId(item.Data, reverseOrder: true);

                if (existingStackableItem == null)
                    return;

                int removeQuantity = Mathf.Min(quantity, existingStackableItem.Quantity);

                existingStackableItem.RemoveQuantity(removeQuantity);
                EventBus.Publish(new ItemStackedEventData(existingStackableItem));

                if (existingStackableItem.Quantity <= 0)
                    inventory.QueryHandler.TryRemoveFromInventory(existingStackableItem, triggerEvent: true);

                quantity -= removeQuantity;
            }
        }

        public void HandleRemoveItemById(Inventory.Core.Inventory inventory, InventoryItem item, int quantity)
        {
            if (item == null) return;

            item.RemoveQuantity(quantity);
            EventBus.Publish(new ItemStackedEventData(item));

            if (item.Quantity <= 0)
            {
                if (item.Quantity <= 0)
                    inventory.QueryHandler.TryRemoveFromInventory(item, triggerEvent: true);
            }
        }
    }
}