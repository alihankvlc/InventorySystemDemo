using InventorySystem.Display;
using InventorySystem.Operators.Base;

namespace InventorySystem.Interfaces
{
    public interface ISlotOperator
    {
        void Execute(SlotDisplayBehaviour droppedBehaviour, SlotBase targetSlot);
    }
}