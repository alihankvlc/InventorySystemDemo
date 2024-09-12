using InventorySystem.Display;
using InventorySystem.Inventory.Core;
using InventorySystem.Operators.Base;
using UnityEngine.Events;

namespace InventorySystem.Inventory.Extensions
{
    public sealed class InventoryItemEvent : UnityEvent<InventoryItem> { }

    public sealed class InventoryItemSlotEvent : UnityEvent<InventoryItem, SlotBase> { }

    public sealed class InventoryItemSlotToSlotEvent : UnityEvent<SlotBase, SlotBase> { }

    public sealed class InventoryItemMergeEvent : UnityEvent<InventoryItem, InventoryItem> { }

    public sealed class InventoryItemSwapEvent : UnityEvent<SlotDisplayBehaviour, SlotDisplayBehaviour> { }

    public static class InventoryNotifier
    {
        public static InventoryItemEvent OnItemAdded = new();
        public static InventoryItemEvent OnItemRemoved = new();
        public static InventoryItemEvent OnItemStacked = new();

        public static InventoryItemSlotEvent OnItemAddedToSlot = new();
        public static InventoryItemSlotEvent OnItemRemovedFromSlot = new();

        public static InventoryItemSlotToSlotEvent ItemMovedBetweenSlots = new();
        public static InventoryItemMergeEvent ItemsMerged = new();
        public static InventoryItemSwapEvent ItemsSwapped = new();
    }
}