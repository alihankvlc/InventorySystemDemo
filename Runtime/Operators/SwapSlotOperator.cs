using InventorySystem.Display;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using InventorySystem.Utility;
using UnityEngine;

namespace InventorySystem.Operators
{
    public class SwapSlotOperator : ISlotOperator
    {
        public void Execute(SlotDisplayBehaviour droppedBehaviour, SlotBase targetSlot)
        {
            SlotDisplayBehaviour targetBehaviour = targetSlot.transform
                .GetComponentInChildren<SlotDisplayBehaviour>();

            Transform targetTransform = targetBehaviour.transform;
            Transform droppedSlotTransform = droppedBehaviour.Slot.transform;

            targetTransform.SetParent(droppedSlotTransform);
            targetBehaviour.DraggableSlotController.SetParentAfterDrag(droppedSlotTransform);

            droppedBehaviour.SetDisplaySlot(targetSlot.transform);

            EventBus.Publish(new ItemSwapEventData(droppedBehaviour, targetBehaviour));
        }
    }
}