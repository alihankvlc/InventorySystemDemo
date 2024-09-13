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
        }

        private void Start()
        {
            InventoryNotifier.ItemsMerged.AddListener(OnItemsMerged);
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

        private void OnItemsMerged(InventoryItem previousItem, InventoryItem targetItem)
        {
            int maxStackSize = (previousItem.Data as IStackable).MaxStackSize;

            int droppedQuantity = previousItem.Quantity;
            int targetQuantity = targetItem.Quantity;

            int totalQuantity = droppedQuantity + targetQuantity;

            if (totalQuantity > maxStackSize) return;

            targetItem.AddQuantity(droppedQuantity);
            previousItem.SetQuantity(0);

            if (previousItem.Quantity <= 0)
            {
                int removeID = previousItem.Id;
                HandleRemoveItemById(removeID);
            }
        }

        private void OnDestroy()
        {
            InventoryNotifier.ItemsMerged.RemoveListener(OnItemsMerged);
        }
    }
}