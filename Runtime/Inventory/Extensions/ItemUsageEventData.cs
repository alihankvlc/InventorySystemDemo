using InventorySystem.Inventory.Core;
using InventorySystem.Operators.Base;
using UnityEngine;

public readonly struct ItemUsageEventData
{
    public readonly InventoryItem _item;
    public readonly SlotBase _slot;

    public ItemUsageEventData(InventoryItem item, SlotBase slot)
    {
        _item = item;
        _slot = slot;
    }
}