using System.Collections.Generic;
using System.Linq;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using InventorySystem.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem.Display
{
    public sealed class SlotDisplayManager : MonoBehaviour
    {
        [SerializeField] private GameObject _displayBehaviourPrefab;
        
        private List<SlotDisplayBehaviour> _behaviours = new();

        private void Awake()
        {
            EventBus.Subscribe<ItemSlotEventData>(ItemAdded);
            EventBus.Subscribe<ItemRemovedEventData>(ItemRemoved);
            EventBus.Subscribe<ItemStackedEventData>(ItemStacked);
            EventBus.Subscribe<ItemSwapEventData>(ItemSwapped);
            EventBus.Subscribe<ItemSlotToSlotEventData>(ItemMovedBetweenSlots);
            EventBus.Subscribe<ItemMergeEventData>(ItemsMerged);
        }

        private void ItemAdded(ItemSlotEventData eventData)
        {
            SlotDisplayBehaviour newBehaviour = Instantiate(_displayBehaviourPrefab,
                eventData._slot.transform).GetComponent<SlotDisplayBehaviour>();

#if UNITY_EDITOR
            newBehaviour.transform.name = $"{eventData._item.Data.Name}_Display";
#endif
            if (newBehaviour != null)
            {
                newBehaviour.InitializeBehaviour(eventData._slot);
                _behaviours.Add(newBehaviour);
            }
        }

        private void ItemRemoved(ItemRemovedEventData eventData)
        {
            SlotDisplayBehaviour behaviour = _behaviours.Find(r => r.BehaviourID == eventData._item.Id);

            if (behaviour != null)
            {
                _behaviours.Remove(behaviour);
                Destroy(behaviour.gameObject);

                behaviour = null;
            }
        }

        private void ItemStacked(ItemStackedEventData eventData)
        {
            SlotDisplayBehaviour behaviour = _behaviours.Find
                (r => r.BehaviourID == eventData._item.Id);

            behaviour?.Display.SetQuantity(eventData._item.Quantity);
        }

        private void ItemMovedBetweenSlots(ItemSlotToSlotEventData eventData)
        {
            SlotDisplayBehaviour behaviour = _behaviours.Find(r => r.Slot == eventData._fromSlot);
            behaviour.SetSlotBase(eventData._toSlot);
        }

        private void ItemsMerged(ItemMergeEventData eventData)
        {
            SlotDisplayBehaviour previousBehaviour = _behaviours.Find(r
                => r.BehaviourID == eventData._fromItem.Id);

            SlotDisplayBehaviour targetBehaviour = _behaviours.Find(r
                => r.BehaviourID == eventData._toItem.Id);

            targetBehaviour?.Display.SetQuantity
                (eventData._fromItem.Quantity + eventData._toItem.Quantity);
        }

        private void ItemSwapped(ItemSwapEventData eventData)
        {
            SlotBase currentSlot = eventData._fromBehaviour.Slot;
            SlotBase nextSlot = eventData._targetBehaviour.Slot;

            eventData._fromBehaviour.SetSlotBase(nextSlot);
            eventData._targetBehaviour.SetSlotBase(currentSlot);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<ItemSlotEventData>(ItemAdded);
            EventBus.Unsubscribe<ItemRemovedEventData>(ItemRemoved);
            EventBus.Unsubscribe<ItemStackedEventData>(ItemStacked);
            EventBus.Unsubscribe<ItemSwapEventData>(ItemSwapped);
            EventBus.Unsubscribe<ItemSlotToSlotEventData>(ItemMovedBetweenSlots);
            EventBus.Unsubscribe<ItemMergeEventData>(ItemsMerged);
        }
    }
}