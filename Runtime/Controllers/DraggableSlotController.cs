using InventorySystem.Display;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem.Controllers
{
    public sealed class DraggableSlotController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private SlotDisplay _display;
        [SerializeField] private Transform _parentAfterDrag; //TODO : Remove serialize field attribute

        public Transform ParentAfterDrag => _parentAfterDrag;

        public void Initialize(SlotDisplay display)
        {
            _display = display;
        }

        public void SetParentAfterDrag(Transform newParent)
        {
            _parentAfterDrag = newParent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parentAfterDrag = transform.parent;

            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            _display.SetSlotVisibilityAndState(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(_parentAfterDrag);
            _display.SetSlotVisibilityAndState(true);
        }
    }
}