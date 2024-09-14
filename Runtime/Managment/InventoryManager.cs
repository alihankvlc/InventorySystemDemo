using System;
using InventorySystem.Controllers;
using InventorySystem.Factories;
using InventorySystem.Inventory.Core;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Utility;
using ItemSystem.Scripts.Data.Sub;
using Sirenix.OdinInspector;
using UnityEngine;

namespace InventorySystem.Managment
{
    public sealed class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory.Core.Inventory _inventory;
        [SerializeField] private KeyCode _windowToggleKey = KeyCode.I;
        [SerializeField] private GameObject _inventoryWindowContent;

        private const KeyCode ESCAPE_KEY = KeyCode.Escape;
        private InventoryWindow _window;

#if UNITY_EDITOR

        #region TEST

        //TODO: REMOVE
        [SerializeField] private Potion _potion;
        [SerializeField] private Rifle _rifle;

        [Button] private void AddPotion(int quantity = 1) => AddItem(_potion, quantity);
        [Button] private void AddRifle(int quantity = 1) => AddItem(_rifle, quantity);

        [Button] private void RemovePotion(int id, int quantity = 1) => RemoveItemByDataId(_potion.ID, quantity);

        // [Button]
        // private void RemoveRifle(int quantity = 1) => RemoveItemByDataId(_rifle.ID, quantity);

        #endregion

#endif
        private void Awake()
        {
            _inventory = new(10);
            _window = new(_inventoryWindowContent);
        }

        private void Start()
        {
            EventBus.Subscribe<ItemMergeEventData>(ItemsMerged);
        }

        private void Update()
        {
            HandleInput();
        }

        public void AddItem(IItem item, int quantity)
        {
            InventoryItem newItem = InventoryItemFactory.CreateItem(item, 1);
            _inventory.AddItem(newItem, quantity);
        }

        public void HandleRemoveItemById(int id, int quantity = 1)
        {
            _inventory.HandleRemoveItemById(id, quantity);
        }

        public void RemoveItemByDataId(int id, int quantity = 1)
        {
            InventoryItem item = _inventory.QueryHandler.FindItemByDataId(id);
            _inventory.RemoveItem(item, quantity);
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(_windowToggleKey) ||
                (_window.IsWindowOpen && Input.GetKeyDown(ESCAPE_KEY)))
                _window.ToggleWindow();
        }

        private void ItemsMerged(ItemMergeEventData eventData)
        {
            int maxStackSize = (eventData._fromItem.Data as IStackable).MaxStackSize;

            int droppedQuantity = eventData._fromItem.Quantity;
            int targetQuantity = eventData._toItem.Quantity;

            int totalQuantity = droppedQuantity + targetQuantity;

            if (totalQuantity > maxStackSize) return;

            eventData._toItem.AddQuantity(droppedQuantity);
            eventData._fromItem.SetQuantity(0);

            if (eventData._fromItem.Quantity <= 0)
            {
                int removeID = eventData._fromItem.Id;
                HandleRemoveItemById(removeID);
            }
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<ItemMergeEventData>(ItemsMerged);
        }
    }
}