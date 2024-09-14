// using System;
// using Unity.VisualScripting;
// using UnityEditor;
// using UnityEngine;
// using UnityEngine.UI;
//
// public sealed class InventoryInitializerEditor : EditorWindow
// {
//     private GameObject _inventorySlotPrefab;
//
//     private Vector2 _inventorySize = new Vector2(540, 600);
//     private Vector2 _gridLayoutCellSize = new Vector2(100, 100);
//     private Vector2 _gridLayoutSpacing = new Vector2(5, 5);
//
//     private RectTransform _inventoryRect;
//     private GridLayoutGroup _inventoryGridLayout;
//
//     private bool _isCreateInventory = false;
//
//     private int _slotSize = 20;
//     private int _inventorySlotSize = 10;
//     private int _hotBarSlotSize;
//
//     [MenuItem("Tools/InventorySystem/Inventory Initializer")]
//     public static void ShowWindow()
//     {
//         GetWindow<InventoryInitializerEditor>("Inventory Initializer");
//     }
//
//     private void OnGUI()
//     {
//         GUILayout.Label("Inventory Settings", EditorStyles.boldLabel);
//         GUILayout.Space(10f);
//
//         _slotSize = EditorGUILayout.IntSlider("Slot Size", _slotSize, 1, 100);
//         _inventorySlotSize = EditorGUILayout.IntSlider("Inventory Slot Size", _inventorySlotSize, 1, 100);
//         _hotBarSlotSize = EditorGUILayout.IntSlider("HotBar Slot Size", _hotBarSlotSize, 1, 100);
//
//         _inventorySlotPrefab =
//             EditorGUILayout.ObjectField("InventorySlot Prefab", _inventorySlotPrefab, typeof(GameObject), false) as GameObject;
//
//         GUIStyle centeredLabelStyle = new GUIStyle(GUI.skin.label);
//         centeredLabelStyle.alignment = TextAnchor.MiddleCenter;
//
//         GUILayout.Space(10f);
//
//         GUILayout.Label("Inventory Size", centeredLabelStyle);
//
//         _inventorySize = EditorGUILayout.Vector2Field(GUIContent.none, _inventorySize);
//
//         GUILayout.Label("GridLayoutGroup Cell Size  ", centeredLabelStyle);
//         _gridLayoutCellSize = EditorGUILayout.Vector2Field(GUIContent.none, _gridLayoutCellSize);
//
//         GUILayout.Label("GridLayoutGroup Spacing Size  ", centeredLabelStyle);
//         _gridLayoutSpacing = EditorGUILayout.Vector2Field(GUIContent.none, _gridLayoutSpacing);
//
//         if (_isCreateInventory)
//         {
//             if (GUILayout.Button("Apply Settings"))
//                 ApplySettings();
//
//             if (GUILayout.Button("Clear Settings"))
//                 ClearSettings();
//
//             if (GUILayout.Button("Destroy Inventory"))
//                 DestroyInventory();
//         }
//         else
//         {
//             if (GUILayout.Button("Create Inventory"))
//             {
//                 if (TryFindCanvas(out Canvas canvas) && TryAddToInventory(canvas))
//                 {
//                     ApplySettings();
//                     _isCreateInventory = true;
//                 }
//             }
//         }
//     }
//
//     private bool TryFindCanvas(out Canvas canvas)
//     {
//         canvas = FindObjectOfType<Canvas>();
//         return canvas != null;
//     }
//
//     private bool TryAddToInventory(Canvas canvas)
//     {
//         if (_inventorySlotPrefab == null)
//         {
//             throw new NullReferenceException(" SlotDisplayPrefab is null");
//             return false;
//         }
//
//         Transform inventory = new GameObject("_Inventory_").transform;
//         inventory.SetParent(canvas.transform);
//
//         _inventoryRect = inventory.AddComponent<RectTransform>();
//         _inventoryGridLayout = _inventoryRect.AddComponent<GridLayoutGroup>();
//
//         _inventoryRect.AddComponent<CanvasGroup>().alpha = 0;
//
//
//         for (int i = 0; i < _slotSize; i++)
//         {
//             GameObject slot = Instantiate(_inventorySlotPrefab, _inventoryRect);
//
//             if (i < _hotBarSlotSize)
//             {
//                HotbarSlot hotbarSlot = slot.AddComponent<HotbarSlot>();
//                hotbarSlot.transform.name = "Hotbar Slot_" + i;
//                 continue;
//             }
//
//            InventorySlot inventorySlot =  slot.AddComponent<InventorySlot>();
//             inventorySlot.transform.name = "Inventory Slot_" + i;
//         }
//
//         return true;
//     }
//
//     private void ApplySettings()
//     {
//         _inventoryRect.sizeDelta = _inventorySize;
//
//         _inventoryRect.anchorMin = new Vector2(0.5f, 0.5f);
//         _inventoryRect.anchorMax = new Vector2(0.5f, 0.5f);
//         _inventoryRect.pivot = new Vector2(0.5f, 0.5f);
//
//         _inventoryRect.anchoredPosition = Vector2.zero;
//
//         _inventoryGridLayout.cellSize = _gridLayoutCellSize;
//         _inventoryGridLayout.spacing = _gridLayoutSpacing;
//     }
//
//     private void ClearSettings()
//     {
//         _inventorySize = Vector3.zero;
//         _gridLayoutCellSize = Vector3.zero;
//         _gridLayoutSpacing = Vector3.zero;
//     }
//
//     private void DestroyInventory()
//     {
//         DestroyImmediate(_inventoryRect.gameObject);
//         _isCreateInventory = false;
//     }
// }