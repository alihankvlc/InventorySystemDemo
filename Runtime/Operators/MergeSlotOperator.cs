using InventorySystem.Display;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class MergeSlotOperator : ISlotOperator
    {
        public void Execute(SlotDisplayBehaviour droppedBehaviour, SlotBase targetSlot)
        {
            if (!targetSlot.IsOccupied) return;
            InventoryNotifier.ItemsMerged?.Invoke(droppedBehaviour.Slot.Item, targetSlot.Item);
        }
    }
}