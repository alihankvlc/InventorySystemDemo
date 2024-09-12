using System.Collections.Generic;
using UnityEditor;

namespace InventorySystem.Utility
{
    public static class IDPool
    {
        private static int _nextID;
        private static readonly Queue<int> _availableIDs;

        static IDPool()
        {
            _availableIDs = new();
        }

        public static int TakeID()
        {
            return _availableIDs.Count > 0 ? _availableIDs.Dequeue() : _nextID++;
        }

        public static void ReleaseID(int id)
        {
            _availableIDs.Enqueue(id);
        }

        [InitializeOnEnterPlayMode]
        private static void Clear()
        {
            _availableIDs.Clear();
            _nextID = 0;
        }
    }
}