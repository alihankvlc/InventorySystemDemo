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

        [Button] private void AddPotion() => AddItem(_potion, 1);
        [Button] private void RemovePotion(int id) => RemoveItem(id);
        [Button] private void AddRifle() => AddItem(_rifle, 1);

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
            InventoryItem newItem = InventoryItemFactory.CreateItem(item, quantity);
            _inventory.AddItem(newItem);
        }

        public void RemoveItem(int id, int quantity = 1)
        {
            InventoryItem existingItem = _inventory.GetInventoryItem(id);

            if (existingItem != null)
            {
                _inventory.RemoveItem(existingItem, quantity);
                IDPool.ReleaseID(existingItem.Id);
            }
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
                RemoveItem(removeID);
            }
        }

        private void OnDestroy()
        {
            InventoryNotifier.ItemsMerged.RemoveListener(OnItemsMerged);
        }
    }
}