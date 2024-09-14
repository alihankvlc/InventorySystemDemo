using System.Collections.Generic;
using System.Linq;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Extensions;
using InventorySystem.Operators;
using InventorySystem.Operators.Base;
using ItemSystem.Scripts.DataManager;
using UnityEngine;

namespace InventorySystem.Inventory.Core
{
    [System.Serializable]
    public sealed class Inventory
    {
        [SerializeField] private List<InventoryItem> _items;

        private readonly Dictionary<System.Type, IInventoryOperator> _operators;
        private readonly InventoryQueryHandler _queryHandler;
        private readonly int _capacity;

        public InventoryQueryHandler QueryHandler => _queryHandler;
        public List<InventoryItem> Items => _items;
        public int Capacity => _capacity;

        public Inventory(int capacity)
        {
            _items = new();
            _queryHandler = new(this);

            _operators = new()
            {
                { typeof(IStackable), new StackableOperator() },
                { typeof(IItem), new NonStackableOperator() }
            };

            _capacity = capacity;
        }

        public void AddItem(InventoryItem inventoryItem, int quantity = 1)
        {
            if (_items.Count >= _capacity) return;

            IInventoryOperator @operator = GetInventoryOperator(inventoryItem.Data);
            ProcessItemAddition(@operator, inventoryItem, quantity);
        }

        public void RemoveItem(InventoryItem inventoryItem, int quantity = 1)
        {
            IInventoryOperator @operator = GetInventoryOperator(inventoryItem.Data);
            ProcessItemRemoval(@operator, inventoryItem, quantity);
        }

        public void HandleRemoveItemById(int id, int quantity = 1)
        {
            InventoryItem existingItem = _items.FirstOrDefault(r => r.Id == id);
            IInventoryOperator @operator = GetInventoryOperator(existingItem.Data);

            ProccesHandleRemoveItemById(@operator, existingItem, quantity);
        }

        private void ProcessItemAddition(IInventoryOperator @operator, InventoryItem inventoryItem, int quantity)
        {
            @operator.AddItem(this, inventoryItem, quantity);
        }

        private void ProcessItemRemoval(IInventoryOperator @operator, InventoryItem inventoryItem, int quantity)
        {
            @operator?.RemoveItem(this, inventoryItem, quantity);
        }

        private void ProccesHandleRemoveItemById(IInventoryOperator @operator, InventoryItem item, int quantity)
        {
            @operator.HandleRemoveItemById(this, item, quantity);
        }

        private IInventoryOperator GetInventoryOperator(IItem item)
        {
            return item is IStackable ? _operators[typeof(IStackable)] : _operators[typeof(IItem)];
        }
    }
}