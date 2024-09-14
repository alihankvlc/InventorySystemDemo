using DG.Tweening;
using InventorySystem.Inventory.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.Display
{
    public sealed class SlotDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _quantityTextMesh;
        [SerializeField] private Image _iconImage;
        [SerializeField] private CanvasGroup _cg;

        private const float DEFAULT_ALPHA = 1f;
        private const float DISABLED_ALPHA = 0.5f;

        public Image IconImage => _iconImage;

        public void SetDisplay(InventoryItem inventoryItem)
        {
            SetQuantity(inventoryItem.Quantity);
            SetIcon(inventoryItem.Data.Icon);
        }

        public void SetQuantity(int quantity)
        {
            _quantityTextMesh.SetText(quantity.ToString());
        }

        public void SetIcon(Sprite icon)
        {
            _iconImage.sprite = icon;
        }

        public void SetSlotVisibilityAndState(bool value)
        {
            _quantityTextMesh.gameObject.SetActive(value);
            _iconImage.raycastTarget = value;

            _cg.alpha = value ? DEFAULT_ALPHA : DISABLED_ALPHA;
        }

        private Tweener _tweener;

        public void ClickAnimation() //TODO: Remove_ClickAnimation
        {
            _tweener?.Kill();
            _tweener = transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.1f).OnComplete(() =>
            {
                _tweener = transform.DOScale(Vector3.one, 0.1f);
            });
        }
    }
}