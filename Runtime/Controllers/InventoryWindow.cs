using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Controllers
{
    public sealed class InventoryWindow
    {
        private CanvasGroup _canvasGroup;

        public bool IsWindowOpen { get; private set; }

        public InventoryWindow(GameObject window)
        {
            if (!window.TryGetComponent(out CanvasGroup cg))
            {
                throw new NullReferenceException("CanvasGroup component is missing!");
                return;
            }

            _canvasGroup = cg;
        }

        public void ToggleWindow()
        {
            IsWindowOpen = !IsWindowOpen;

            _canvasGroup.alpha = IsWindowOpen ? 1 : 0;
            _canvasGroup.blocksRaycasts = IsWindowOpen;
        }
    }
}