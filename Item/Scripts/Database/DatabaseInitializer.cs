using Sisus.Init;
using UnityEngine;

public interface IDatabaseService
{
    public ItemDatabase ItemDatabase { get; }
}

public sealed class DatabaseInitializer : MonoBehaviour<ItemDatabase>, IDatabaseService
{
    private ItemDatabase _itemDatabase;
    public ItemDatabase ItemDatabase => _itemDatabase;

    protected override void Init(ItemDatabase argument)
    {
        _itemDatabase = argument;
        _itemDatabase.Initialize();
    }
}