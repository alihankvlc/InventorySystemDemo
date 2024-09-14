using InventorySystem.Display;
using InventorySystem.Factories;
using InventorySystem.Interfaces;
using InventorySystem.Operators.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.Events
{
    public sealed class SlotDropEventHandler : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject droppedObject = eventData.pointerDrag;

            SlotDisplayBehaviour behaviour = droppedObject?
                .GetComponent<SlotDisplayBehaviour>();

            SlotBase targetSlot = GetComponent<SlotBase>();

            if (behaviour.Slot.Index == targetSlot.Index)
            {
                return;
            }

            ISlotOperator @operator = SlotOperatorFactory.GetSlotOperator(behaviour, targetSlot);
            @operator?.Execute(behaviour, targetSlot);
        }
    }
}