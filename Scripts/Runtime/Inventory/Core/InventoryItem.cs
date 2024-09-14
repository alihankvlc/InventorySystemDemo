namespace InventorySystem.Inventory.Core
{
    public sealed class InventoryItem
    {
        public IItem Data { get; private set; }
        public int Quantity { get; private set; }
        public int Id { get; private set; }

        public InventoryItem(IItem item, int quantity, int id)
        {
            Data = item;
            Quantity = quantity;
            Id = id;
        }

        public void AddQuantity(int quantity = 1) => Quantity += quantity;
        public void RemoveQuantity(int quantity = 1) => Quantity -= quantity;
        public void SetQuantity(int quantity) => Quantity = quantity;
    }
}