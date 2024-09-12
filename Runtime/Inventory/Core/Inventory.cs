using System.Collections.Generic;
using InventorySystem.Interfaces;
using InventorySystem.Operators;
using UnityEngine;

namespace InventorySystem.Inventory.Core
{
    [System.Serializable]
    public sealed class Inventory
    {
       [SerializeField] private  List<InventoryItem> _items;

        private readonly Dictionary<System.Type, IInventoryOperator> _operators;
        private readonly int _capacity;

        public Inventory(int capacity)
        {
            _capacity = capacity;
            _items = new();
            _operators = new()
            {
                { typeof(IStackable), new StackableOperator() },
                { typeof(IItem), new NonStackableOperator() }
            };
        }

        public void AddItem(InventoryItem inventoryItem, int quantity = 1)
        {
            IInventoryOperator @operator = GetInventoryOperator(inventoryItem.Data);
            @operator.AddItem(this, inventoryItem, quantity);
        }

        public void RemoveItem(InventoryItem inventoryItem, int quantity = 1)
        {
            IInventoryOperator @operator = GetInventoryOperator(inventoryItem.Data);
            @operator?.RemoveItem(this, inventoryItem, quantity, () => { _items.Remove(inventoryItem); });
        }

        public bool TryAddToInventory(InventoryItem inventoryItem)
        {
            if (_items.Count >= _capacity) return false;

            _items.Add(inventoryItem);
            return true;
        }

        public bool TryFindStackableItem(int id, out InventoryItem inventoryItem)
        {
            InventoryItem existinItem = _items.Find(r =>
                r.Data.ID == id && r.Data is IStackable stackable && r.Quantity < stackable.MaxStackSize);

            inventoryItem = existinItem;
            return inventoryItem != null;
        }

        public InventoryItem GetInventoryItem(int id)
        {
            return _items.Find(r => r.Id == id);
        }

        private IInventoryOperator GetInventoryOperator(IItem item)
        {
            return item is IStackable ? _operators[typeof(IStackable)] : _operators[typeof(IItem)];
        }
    }
}