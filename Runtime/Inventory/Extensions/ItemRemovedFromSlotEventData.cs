﻿using InventorySystem.Inventory.Core;
using InventorySystem.Operators.Base;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemRemovedFromSlotEventData
    {
        public readonly InventoryItem _item;
        public readonly SlotBase _slot;

        public ItemRemovedFromSlotEventData(InventoryItem item, SlotBase slot)
        {
            _item = item;
            _slot = slot;
        }
    }
}