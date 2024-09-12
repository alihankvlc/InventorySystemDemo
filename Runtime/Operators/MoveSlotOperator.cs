using InventorySystem.Display;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class MoveSlotOperator : ISlotOperator
    {
        public void Execute(SlotDisplayBehaviour droppedBehaviour, SlotBase targetSlot)
        {
            droppedBehaviour.SetDisplaySlot(targetSlot.transform);
            InventoryNotifier.ItemMovedBetweenSlots?.Invoke(droppedBehaviour.Slot,targetSlot);
        }
    }
}