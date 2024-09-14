using InventorySystem.Operators.Base;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemSlotToSlotEventData
    {
        public readonly SlotBase _fromSlot;
        public readonly SlotBase _toSlot;

        public ItemSlotToSlotEventData(SlotBase fromSlot, SlotBase toSlot)
        {
            _fromSlot = fromSlot;
            _toSlot = toSlot;
        }
    }
}