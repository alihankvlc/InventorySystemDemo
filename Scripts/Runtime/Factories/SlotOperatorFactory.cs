using System.Collections.Generic;
using InventorySystem.Display;
using InventorySystem.Interfaces;
using InventorySystem.Inventory.Core;
using InventorySystem.Operators;
using InventorySystem.Operators.Base;

namespace InventorySystem.Factories
{
    public enum SlotOperatorType
    {
        Merge,
        Swap,
        Move
    }

    public static class SlotOperatorFactory
    {
        private static readonly Dictionary<SlotOperatorType, ISlotOperator> _operatorCache = new()
        {
            { SlotOperatorType.Merge, new MergeSlotOperator() },
            { SlotOperatorType.Swap, new SwapSlotOperator() },
            { SlotOperatorType.Move, new MoveSlotOperator() }
        };

        public static ISlotOperator GetSlotOperator(SlotDisplayBehaviour displayBehaviour, SlotBase targetSlot)
        {
            if (!targetSlot.IsOccupied)
                return GetCachedOperator(SlotOperatorType.Move);

            InventoryItem targetSlotItem = targetSlot.Item;
            
            if (targetSlotItem == null)
                return GetCachedOperator(SlotOperatorType.Move);

            InventoryItem droppedItem = displayBehaviour?.Slot?.Item;

            if (droppedItem == null)
            {
                throw new System.NullReferenceException("Dropped item is  null.");
            }

            IStackable stackableItem = targetSlotItem.Data as IStackable;

            if (stackableItem == null)
                return GetCachedOperator(SlotOperatorType.Swap);

            int droppedQuantity = droppedItem.Quantity;
            int targetQuantity = targetSlotItem.Quantity;

            int totalQuantity = droppedQuantity + targetQuantity;

            bool isSameStackSize = totalQuantity > stackableItem.MaxStackSize;
            bool isSameItem = targetSlotItem.Data == droppedItem.Data;

            if (!isSameItem || isSameStackSize)
                return GetCachedOperator(SlotOperatorType.Swap);

            return GetCachedOperator(SlotOperatorType.Merge);
        }


        private static ISlotOperator GetCachedOperator(SlotOperatorType operatorType)
        {
            return _operatorCache[operatorType];
        }
    }
}