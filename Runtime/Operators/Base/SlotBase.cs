using InventorySystem.Inventory.Core;
using ItemSystem.Scripts.DataManager;
using UnityEngine;

namespace InventorySystem.Operators.Base
{
    public sealed class SlotBase : MonoBehaviour
    {
        public InventoryItem Item;
        public bool IsOccupied { get; private set; }
        public int Index { get; private set; }

        public void AddItemToSlot(InventoryItem item)
        {
            Item = item;
            IsOccupied = true;
        }

        public void RemoveItemFromSlot()
        {
            Item = null;
            IsOccupied = false;
        }

        public void UpdateSlot(InventoryItem newItem)
        {
            Item = null;
            Item = newItem;
        }

        public void SetIndex(int index)
        {
            Index = index;
        }
    }
}