﻿using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.Panels
{
    public abstract class PopupPanel : PanelBase
    {
        [SerializeField] private Button _closeButton;

        protected override void OnShow()
        {
            base.OnShow();
            _closeButton.onClick.AddListener(Hide);
        }

        protected override void OnHide()
        {
            base.OnHide();
            _closeButton.onClick.RemoveListener(Hide);
        }
    }
}