using UnityEngine;

namespace Game.Runtime.Infrastructure.Panels
{
    public abstract class PanelBase : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        
        public bool IsActive { get; private set; }

        public void Show()
        {
            _panel.gameObject.SetActive(true);
            IsActive = true;
            OnShow();
        }

        protected virtual void OnShow(){}

        public void Hide()
        {
            _panel.gameObject.SetActive(false);
            IsActive = false;
            OnHide();
        }

        protected virtual void OnHide(){}
    }
}