﻿using DG.Tweening;
using InventorySystem.Controllers;
using InventorySystem.Operators.Base;
using InventorySystem.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.Display
{
    [RequireComponent(typeof(SlotDisplay))]
    [RequireComponent(typeof(DraggableSlotController))]
    public sealed class SlotDisplayBehaviour : MonoBehaviour, IPointerEnterHandler
    {
        private SlotDisplay _slotDisplay;
        private SlotBase _slot;

        private DraggableSlotController _draggableSlotController;

        private int _behaviourId;

        public SlotDisplay Display => _slotDisplay;
        public DraggableSlotController DraggableSlotController => _draggableSlotController;
        public SlotBase Slot => _slot;

        public int BehaviourID => _behaviourId;

        private void Awake()
        {
            _slotDisplay = GetComponent<SlotDisplay>();
            _draggableSlotController = GetComponent<DraggableSlotController>();
        }

        private void Start()
        {
            _draggableSlotController.Initialize(_slotDisplay);
        }

        public void InitializeBehaviour(SlotBase slotBase) //TODO: param change to int
        {
            _slot = slotBase;
            _behaviourId = slotBase.Item.Id;

            _slotDisplay.SetDisplay(slotBase.Item);
        }

        public void SetSlotBase(SlotBase newSlot)
        {
            _slot = newSlot;
        }

        public void SetDisplaySlot(Transform newParent)
        {
            _draggableSlotController.SetParentAfterDrag(newParent);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_slot.Item.Data is IUseable)
            {
                EventBus.Publish(new ItemUsageEventData(_slot.Item, _slot));
            }
        }

        public void OnPointerEnter(PointerEventData eventData) //TODO: Remove_ClickAnimation
        {
            _slotDisplay.ClickAnimation();
        }
    }
}