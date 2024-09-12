using System.Linq;
using InventorySystem.Display;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem.Managment
{
    public sealed class SlotManager : MonoBehaviour
    {
        [SerializeField, InlineEditor] private SlotBase[] _slots;

        private void Start()
        {
            for (var i = 0; i < _slots.Length; i++) _slots[i].SetIndex(i);

            InventoryNotifier.OnItemAdded.AddListener(OnItemAdded);
            InventoryNotifier.OnItemRemoved.AddListener(OnItemRemoved);
            InventoryNotifier.ItemMovedBetweenSlots.AddListener(OnItemMovedBetweenSlots);
            InventoryNotifier.ItemsSwapped.AddListener(OnItemSwapped);
        }

        public void OnItemAdded(InventoryItem item)
        {
            SlotBase availableSlotBase = _slots.FirstOrDefault(r => !r.IsOccupied);
            if (availableSlotBase != null)
            {
                availableSlotBase.AddItemToSlot(item);
                InventoryNotifier.OnItemAddedToSlot?.Invoke(item, availableSlotBase);
            }
        }

        public void OnItemRemoved(InventoryItem item)
        {
            SlotBase availableSlotBase = _slots.FirstOrDefault(r => r.IsOccupied && r.Item.Id == item.Id);
            if (availableSlotBase != null)
            {
                availableSlotBase.RemoveItemFromSlot();
                InventoryNotifier.OnItemRemovedFromSlot?.Invoke(item, availableSlotBase);
            }
        }

        private void OnItemMovedBetweenSlots(SlotBase previousSlot, SlotBase targetSlot)
        {
            InventoryItem tempItem = previousSlot.Item;

            previousSlot.RemoveItemFromSlot();
            targetSlot.AddItemToSlot(tempItem);
        }

        private void OnItemSwapped(SlotDisplayBehaviour droppedBehaviour, SlotDisplayBehaviour targetBehaviour)
        {
            SlotBase currentSlot = droppedBehaviour.Slot;
            SlotBase nextSlot = targetBehaviour.Slot;

            InventoryItem currentItem = currentSlot.Item;
            InventoryItem nextItem = nextSlot.Item;

            currentSlot.UpdateSlot(nextItem);
            nextSlot.UpdateSlot(currentItem);
        }
        
        private void OnDestroy()
        {
            InventoryNotifier.OnItemAdded.RemoveListener(OnItemAdded);
            InventoryNotifier.OnItemRemoved.AddListener(OnItemRemoved);
            InventoryNotifier.ItemMovedBetweenSlots.RemoveListener(OnItemMovedBetweenSlots);
            InventoryNotifier.ItemsSwapped.RemoveListener(OnItemSwapped);
        }
    }
}