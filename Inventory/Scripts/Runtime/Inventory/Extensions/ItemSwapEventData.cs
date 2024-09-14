using InventorySystem.Display;

namespace InventorySystem.Inventory.Extensions
{
    public readonly struct ItemSwapEventData
    {
        public readonly SlotDisplayBehaviour _fromBehaviour;
        public readonly SlotDisplayBehaviour _targetBehaviour;

        public ItemSwapEventData(SlotDisplayBehaviour fromBehaviour, SlotDisplayBehaviour targetBehaviour)
        {
            _fromBehaviour = fromBehaviour;
            _targetBehaviour = targetBehaviour;
        }
    }
}