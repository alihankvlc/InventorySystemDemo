using ItemSystem.Scripts.DataManager;
using UnityEngine;

public abstract class ConsumableData : ItemData, IStackable, IConsumable, IUseable
{
    [SerializeField] private int _stackSize = 1;

    public override ItemType Type => ItemType.Consumable;
    public int MaxStackSize => _stackSize;

    public abstract void Consume(int amount = 1);

    public void Use(params object[] args)
    {
        if (args.Length != 1 && args[0] != typeof(int))
        {
            Debug.LogError($"Invalid args for {Name}.");
            return;
        }

        Consume((int)args[0]);
    }
}