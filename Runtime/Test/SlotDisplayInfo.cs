#if UNITY_EDITOR
using System;
using InventorySystem.Operators.Base;
using TMPro;
using UnityEngine;

public sealed class SlotDisplayInfo : MonoBehaviour
{
    [SerializeField] private SlotBase _slotBase;

    [SerializeField] private CanvasGroup _cg;
    [SerializeField] private TextMeshProUGUI _slotInItemNameTextMesh;
    [SerializeField] private TextMeshProUGUI _slotIndexTextMesh;

    private void Start() { }

    private void Update()
    {
        _slotIndexTextMesh.color = _slotBase.IsOccupied ? Color.yellow : Color.red;
        _cg.alpha = _slotBase.IsOccupied ? 1 : 0.5f;
        _slotIndexTextMesh.SetText($"Slot: {_slotBase.Index}");

        if (_slotBase.IsOccupied)
        {
            _slotInItemNameTextMesh.SetText($"{_slotBase.Item.Data.Name}: x{_slotBase.Item.Quantity}");
        }
        else
        {
            _slotInItemNameTextMesh.SetText("empty");
        }
    }
}
#endif