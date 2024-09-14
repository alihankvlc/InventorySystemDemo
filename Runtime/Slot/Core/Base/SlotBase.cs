using InventorySystem.Inventory.Core;
using ItemSystem.Scripts.DataManager;
using UnityEngine.EventSystems;
using UnityEngine;

public enum SlotType
{
    HotBar,
    Inventory,
}

namespace InventorySystem.Operators.Base
{
    public abstract class SlotBase : MonoBehaviour
    {
        public InventoryItem Item;
        public bool IsOccupied { get; private set; }
        public int Index { get; private set; }
        public virtual SlotType SlotType { get; protected set; }

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

        public void SetSlot(int index)
        {
            Index = index;
        }
    }
}