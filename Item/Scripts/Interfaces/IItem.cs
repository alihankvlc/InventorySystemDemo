using UnityEngine;

public interface IItem
{
    public int ID { get; }
    public string Name { get; }
    public string Description { get; }
    public Sprite Icon { get; }
}