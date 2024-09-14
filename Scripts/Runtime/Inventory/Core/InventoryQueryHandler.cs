using System.Collections.Generic;
using System.Linq;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Utility;
using UnityEngine;

public sealed class InventoryQueryHandler
{
    private readonly Inventory _inventory;

    public InventoryQueryHandler(Inventory inventory)
    {
        _inventory = inventory;
    }

    public bool TryAddToInventory(InventoryItem inventoryItem, bool triggerEvent = true)
    {
        if (_inventory.Items.Count >= _inventory.Capacity) return false;

        _inventory.Items.Add(inventoryItem);

        if (!triggerEvent) return true;
        EventBus.Publish(new ItemAddedEventData(inventoryItem));
        return true;
    }

    public bool TryRemoveFromInventory(InventoryItem inventoryItem, bool triggerEvent = true)
    {
        if (_inventory.Items.Contains(inventoryItem))
        {
            _inventory.Items.Remove(inventoryItem);
            EventBus.Publish(new ItemRemovedEventData(inventoryItem));
        }

        return false;
    }

    public InventoryItem FindExistingStackableItem(int dataId)
    {
        return _inventory.Items.Find(r =>
            r.Data.ID == dataId && r.Quantity < (r.Data as IStackable).MaxStackSize);
    }

    public InventoryItem FindInventoryItemById(int id)
    {
        return _inventory.Items.Find(r => r.Id == id);
    }

    public InventoryItem FindItemByDataId(IItem item, bool reverseOrder = false)
    {
        IEnumerable<InventoryItem> query = _inventory.Items.Where(r => r.Data == item);

        return reverseOrder
            ? query.OrderByDescending(r => r.Id).FirstOrDefault()
            : query.FirstOrDefault();
    }

    public InventoryItem FindItemByDataId(int id, bool reverseOrder = false)
    {
        IEnumerable<InventoryItem> query = _inventory.Items.Where(r => r.Data.ID == id);

        return reverseOrder
            ? query.OrderByDescending(r => r.Id).FirstOrDefault()
            : query.FirstOrDefault();
    }
}