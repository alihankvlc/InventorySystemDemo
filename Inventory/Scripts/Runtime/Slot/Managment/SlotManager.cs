using System.Linq;
using InventorySystem.Display;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using InventorySystem.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem.Managment
{
    public sealed class SlotManager : MonoBehaviour
    {
        [SerializeField, InlineEditor] private SlotBase[] _slots;

        private void Awake()
        {
            for (var i = 0; i < _slots.Length; i++) _slots[i].SetSlot(i);

            EventBus.Subscribe<ItemAddedEventData>(ItemAdded);
            EventBus.Subscribe<ItemRemovedEventData>(ItemRemoved);
            EventBus.Subscribe<ItemSlotToSlotEventData>(ItemMovedBetweenSlots);
            EventBus.Subscribe<ItemSwapEventData>(ItemSwapped);
        }

        public void ItemAdded(ItemAddedEventData eventData)
        {
            SlotBase availableSlotBase = _slots.FirstOrDefault(r => !r.IsOccupied);

            if (availableSlotBase != null)
            {
                availableSlotBase.AddItemToSlot(eventData._item);
                EventBus.Publish(new ItemSlotEventData(eventData._item, availableSlotBase));
            }
        }

        public void ItemRemoved(ItemRemovedEventData eventData)
        {
            SlotBase availableSlotBase = _slots.FirstOrDefault(r => r.IsOccupied && r.Item.Id == eventData._item.Id);
            if (availableSlotBase != null)
            {
                availableSlotBase.RemoveItemFromSlot();
                EventBus.Publish(new ItemRemovedFromSlotEventData(eventData._item, availableSlotBase));
            }
        }

        private void ItemMovedBetweenSlots(ItemSlotToSlotEventData eventData)
        {
            InventoryItem tempItem = eventData._fromSlot.Item;

            eventData._fromSlot.RemoveItemFromSlot();
            eventData._toSlot.AddItemToSlot(tempItem);
        }

        private void ItemSwapped(ItemSwapEventData eventData)
        {
            SlotBase currentSlot = eventData._fromBehaviour.Slot;
            SlotBase nextSlot = eventData._targetBehaviour.Slot;

            InventoryItem currentItem = currentSlot.Item;
            InventoryItem nextItem = nextSlot.Item;

            currentSlot.UpdateSlot(nextItem);
            nextSlot.UpdateSlot(currentItem);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<ItemAddedEventData>(ItemAdded);
            EventBus.Unsubscribe<ItemRemovedEventData>(ItemRemoved);
            EventBus.Unsubscribe<ItemSlotToSlotEventData>(ItemMovedBetweenSlots);
            EventBus.Unsubscribe<ItemSwapEventData>(ItemSwapped);
        }
    }
}