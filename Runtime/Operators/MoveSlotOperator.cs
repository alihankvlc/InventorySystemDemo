using InventorySystem.Display;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using InventorySystem.Utility;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class MoveSlotOperator : ISlotOperator
    {
        public void Execute(SlotDisplayBehaviour droppedBehaviour, SlotBase targetSlot)
        {
            droppedBehaviour.SetDisplaySlot(targetSlot.transform);
            EventBus.Publish(new ItemSlotToSlotEventData(droppedBehaviour.Slot, targetSlot));
        }
    }
}