using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.Items
{
    public class ItemView : MonoBehaviour
    {
        public string Id => id;
        [SerializeField] 
        private string id;
        [SerializeField] 
        private Button _button;
        [SerializeField] 
        private Image _mainIcon;
        [Header("UNLOCKED STATE")]
        [SerializeField] 
        private GameObject _unlockedState;
        [SerializeField] 
        private GameObject _progressContainer;
        [SerializeField] 
        private Image _progressBarImage;
        [SerializeField] 
        private Image _rewardResourceIcon;
        [SerializeField] 
        private Button _rewardButton;
        [Header("LOCKED STATE")]
        [SerializeField] 
        private GameObject _lockedState;
        [SerializeField] 
        private TextMeshProUGUI _unlockPriceText;
        [SerializeField] 
        private Image _unlockResourceIcon;

        private IItemViewPresenter _presenter;
        private IDisposable _disposables;
        
        private void OnDestroy()
        {
            Dispose();
        }

        public void SetPresenter(IItemViewPresenter presenter)
        {
            _presenter = presenter;
            Initialize();
            UpdateView();
        }

        private void Initialize()
        {
            _disposables = Disposable.Combine(
                _presenter,
                _presenter.IsLockedState.Subscribe(isLocked => SetLockedState(isLocked)),
                _presenter.IncomeProgress.Subscribe(value => OnProgressValueChanged(value))
            );
            _button.onClick.AddListener(_presenter.OnItemClick);
            _rewardButton.onClick.AddListener(_presenter.OnIncomeClick);
        }

        private void UpdateView()
        {
            SetLockedState(_presenter.IsLockedState.Value);
            _unlockPriceText.text = _presenter.GetUnlockPriceText();
            _unlockResourceIcon.sprite = _presenter.GetUnlockResourceSprite();
            _rewardResourceIcon.sprite = _presenter.GetIncomeResourceSprite();
        }

        private void OnProgressValueChanged(float progress)
        {
            _progressBarImage.fillAmount = progress;
        }

        private void SetLockedState(bool isLocked)
        {
            //TODO change Item.States
            
            _unlockedState.SetActive(!isLocked);
            _progressContainer.SetActive(!isLocked);
            _rewardResourceIcon.gameObject.SetActive(!isLocked);
            _lockedState.SetActive(isLocked);
            _mainIcon.sprite = _presenter.GetMainSprite();
        }

        private void Dispose()
        {
            _disposables?.Dispose();
            _button.onClick.RemoveAllListeners();
            _rewardButton.onClick.RemoveAllListeners();
        }
    }
}