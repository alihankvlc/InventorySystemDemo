using System.Collections.Generic;
using System.Linq;
using ItemSystem.Scripts.DataManager;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ItemSystem/ItemDatabase", order = 0)]
public sealed class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<ItemData> _items;
    private Dictionary<int, ItemData> _itemDictionary = new();

    public void Initialize()
    {
        _itemDictionary = _items.ToDictionary(r => r.ID);
    }

    public void AddData(ItemData data)
    {
        _items.Add(data);
        _itemDictionary[data.ID] = data;
    }

    public void RemoveData(ItemData data)
    {
        _items.Remove(data);
        _itemDictionary.Remove(data.ID);
    }

    public bool TryGetData(int id, out ItemData data)
    {
        return _itemDictionary.TryGetValue(id, out data);
    }

    public ItemData GetData(int id)
    {
        return _itemDictionary.TryGetValue(id, out ItemData existingData) ? existingData : null;
    }

    public bool HasData(int id)
    {
        return _itemDictionary.ContainsKey(id);
    }

#if UNITY_EDITOR
    [ButtonGroup("Database")]
    [Button("Populate Data")]
    public void Populate()
    {
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(ItemData).Name}");

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var obj = AssetDatabase.LoadAssetAtPath<ItemData>(path);

            if (obj != null) AddData(obj);
        }
    }
#endif

#if UNITY_EDITOR
    [ButtonGroup("Database")]
    [Button("Clear All Data")]
    public void Clear()
    {
        _items.Clear();
        _itemDictionary.Clear();
    }
#endif
}