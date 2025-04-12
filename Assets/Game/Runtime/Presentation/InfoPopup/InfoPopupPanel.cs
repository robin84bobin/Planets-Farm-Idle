using Game.Runtime.Presentation.Panels;
using TMPro;
using UnityEngine;

namespace Game.Runtime.Presentation.InfoPopup
{
    public class InfoPopupPanel : PopupPanel
    {
        [SerializeField] private TextMeshProUGUI _infoText;

        public void SetPresenter(InfoPopupPresenter presenter)
        {
            _infoText.text = presenter.InfoText;
        }
    }
}