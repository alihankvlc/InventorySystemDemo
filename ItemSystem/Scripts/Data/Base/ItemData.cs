using UnityEngine;

namespace ItemSystem.Scripts.DataManager
{
    public enum ItemType
    {
        Weapon,
        Consumable
    }

    public abstract class ItemData : ScriptableObject, IItem, IWeightable
    {
#if UNITY_EDITOR
        [Multiline, Space] public string EDITOR_DESCRIPTION;
#endif
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _weight;
        public int ID => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public float Weight => _weight;

        public virtual ItemType Type { get; protected set; }
    }
}