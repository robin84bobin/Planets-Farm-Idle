using System;
using Game.Runtime.Presentation.Panels;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.TopPanel
{
    public class TopPanel : PanelBase
    {
        [SerializeField] 
        private TextMeshProUGUI _softCurrencyText;
        [SerializeField] 
        private Image _softCurrencyImage;
        
        private ITopPanelPresenter _presenter;
        private IDisposable _disposables;

        private void OnDestroy()
        {
            _disposables?.Dispose();
        }

        public void SetPresenter(ITopPanelPresenter presenter)
        {
            _presenter = presenter;

            _softCurrencyImage.sprite = _presenter.SoftCurrencySprite;
            _disposables = Disposable.Combine(
                    presenter,
                    presenter.SoftCurrencyValueText.Subscribe(x => _softCurrencyText.text = x)
                );
        }
    }
}
