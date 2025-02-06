using Game.Runtime.Infrastructure.Panels;
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

        public void SetPresenter(IItemPopupPresenter presenter)
        {
            _presenter = presenter;
        }
    }
}