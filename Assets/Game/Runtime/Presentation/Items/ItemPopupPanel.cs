using System;
using Game.Runtime.Infrastructure.Panels;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.Items
{
    public class ItemPopupPanel : PopupPanel
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _populationText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _incomeText;
        [SerializeField] private Image _incomeResourceIcon;
        [SerializeField] private Image _upgradePriceResourceIcon;
        [SerializeField] private TextMeshProUGUI _upgradeButtonText;
        [SerializeField] private TextMeshProUGUI _upgradePriceText;

        [SerializeField] private Button _upgradeButton;

        private IItemPopupPresenter _presenter;

        private IDisposable _disposables;

        private void OnDestroy()
        {
            Dispose();
        }

        public void SetPresenter(IItemPopupPresenter presenter)
        {
            _presenter = presenter;
            Initialize();
        }

        private void Initialize()
        {
            _disposables = Disposable.Combine(
                _presenter,
                _presenter.UpgradeIncomeResourceSprite.Subscribe(value => _incomeResourceIcon.sprite = value),
                _presenter.UpgradePriceResourceSprite.Subscribe(value => _upgradePriceResourceIcon.sprite = value),
                _presenter.UpgradePriceText.Subscribe(value => _upgradePriceText.text = value),
                _presenter.PopulationValueText.Subscribe(value => _populationText.text = value)
            );
            _upgradeButton.onClick.AddListener(_presenter.OnUpgradeClick);
            
            _headerText.text = _presenter.GetHeaderText();
            _itemIcon.sprite = _presenter.GetMainSprite();
        }

        protected override void OnHide()
        {
            base.OnHide();
            Dispose();
        }

        private void Dispose()
        {
            _upgradeButton.onClick.RemoveAllListeners();
            _disposables?.Dispose();
        }
    }
}