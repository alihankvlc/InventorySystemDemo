using System.Collections.Generic;
using System.Linq;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators.Base;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem.Display
{
    public sealed class SlotDisplayManager : MonoBehaviour
    {
        [SerializeField] private GameObject _displayBehaviourPrefab;
        [SerializeField, InlineEditor] private List<SlotDisplayBehaviour> _behaviours = new(); //TODO: public to private

        private void Awake()
        {
            InventoryNotifier.OnItemStacked.AddListener(OnItemStacked);
            InventoryNotifier.OnItemAddedToSlot.AddListener(OnItemAdded);
            InventoryNotifier.OnItemRemovedFromSlot.AddListener(OnItemRemoved);
            InventoryNotifier.ItemMovedBetweenSlots.AddListener(OnItemMovedBetweenSlots);
            InventoryNotifier.ItemsSwapped.AddListener(OnItemSwapped);
            InventoryNotifier.ItemsMerged.AddListener(OnItemsMerged);
        }

        private void OnItemAdded(InventoryItem item, SlotBase slotBase)
        {
            SlotDisplayBehaviour newBehaviour = Instantiate(_displayBehaviourPrefab,
                slotBase.transform).GetComponent<SlotDisplayBehaviour>();

#if UNITY_EDITOR
            newBehaviour.transform.name = $"{item.Data.Name}_Display";
#endif
            if (newBehaviour != null)
            {
                newBehaviour.InitializeBehaviour(slotBase);
                _behaviours.Add(newBehaviour);
            }
        }

        private void OnItemRemoved(InventoryItem item, SlotBase slotBase)
        {
            SlotDisplayBehaviour behaviour = _behaviours.Find(r => r.BehaviourID == item.Id);

            if (behaviour != null)
            {
                _behaviours.Remove(behaviour);
                Destroy(behaviour.gameObject);
                
                behaviour = null;
            }
        }

        private void OnItemStacked(InventoryItem item)
        {
            SlotDisplayBehaviour behaviour = _behaviours.Find
                (r => r.BehaviourID == item.Id);

            behaviour?.Display.SetQuantity(item.Quantity);
        }

        private void OnItemMovedBetweenSlots(SlotBase previousSlot, SlotBase targetSlot)
        {
            SlotDisplayBehaviour behaviour = _behaviours.Find(r => r.Slot == previousSlot);
            behaviour.SetSlotBase(targetSlot);
        }

        private void OnItemSwapped(SlotDisplayBehaviour droppedBehaviour, SlotDisplayBehaviour targetBehaviour)
        {
            SlotBase currentSlot = droppedBehaviour.Slot;
            SlotBase nextSlot = targetBehaviour.Slot;

            droppedBehaviour.SetSlotBase(nextSlot);
            targetBehaviour.SetSlotBase(currentSlot);
        }

        private void OnItemsMerged(InventoryItem previousItem, InventoryItem targetItem)
        {
            SlotDisplayBehaviour previousBehaviour = _behaviours.Find(r
                => r.BehaviourID == previousItem.Id);

            SlotDisplayBehaviour targetBehaviour = _behaviours.Find(r
                => r.BehaviourID == targetItem.Id);

            previousBehaviour?.Display.SetQuantity(previousItem.Quantity);
            targetBehaviour?.Display.SetQuantity(targetItem.Quantity);
        }

        private void OnDestroy()
        {
            InventoryNotifier.OnItemStacked.RemoveListener(OnItemStacked);
            InventoryNotifier.OnItemAddedToSlot.RemoveListener(OnItemAdded);
            InventoryNotifier.OnItemRemovedFromSlot.RemoveListener(OnItemRemoved);
            InventoryNotifier.ItemMovedBetweenSlots.RemoveListener(OnItemMovedBetweenSlots);
            InventoryNotifier.ItemsSwapped.RemoveListener(OnItemSwapped);
            InventoryNotifier.ItemsMerged.RemoveListener(OnItemsMerged);
        }
    }
}