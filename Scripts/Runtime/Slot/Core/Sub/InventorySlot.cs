using InventorySystem.Operators.Base;
using UnityEngine;

public sealed class InventorySlot : SlotBase
{
    public override SlotType SlotType => SlotType.Inventory;
}