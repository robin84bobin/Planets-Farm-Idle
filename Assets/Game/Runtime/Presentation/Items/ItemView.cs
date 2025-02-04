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
            UnBindPresenter();
            _disposables.Dispose();
        }

        public void SetPresenter(IItemViewPresenter presenter)
        {
            _presenter = presenter;
            BindPresenter();
            UpdateView();
        }

        private void BindPresenter()
        {
            _disposables = Disposable.Combine(
                _presenter.IsLocked.Subscribe(isLocked => SetLockedState(isLocked)),
                _presenter.IncomeProgress.Subscribe(value => OnProgressValueChanged(value))
            );
            _button.onClick.AddListener(_presenter.OnItemClick);
            _rewardButton.onClick.AddListener(_presenter.OnRewardClick);
        }

        private void UnBindPresenter()
        {
            _button.onClick.RemoveListener(_presenter.OnItemClick);
            _rewardButton.onClick.RemoveListener(_presenter.OnRewardClick);
        }

        private void UpdateView()
        {
            _mainIcon.sprite = _presenter.GetMainSprite();
            _unlockPriceText.text = _presenter.GetUnlockPriceText();
            _unlockResourceIcon.sprite = _presenter.GetUnlockResourceSprite();
            _rewardResourceIcon.sprite = _presenter.GetRewardResourceSprite();
            SetLockedState(_presenter.IsLocked.Value);
        }

        private void OnProgressValueChanged(float progress)
        {
            _progressBarImage.fillAmount = progress;
        }

        private void OnLockedStateChanged(bool isLocked)
        {
            SetLockedState(isLocked);
        }

        private void SetLockedState(bool isLocked)
        {
            _unlockedState.SetActive(!isLocked);
            _lockedState.SetActive(isLocked);
        }

        private void OnMainImageChanged(Sprite newSprite)
        {
            _mainIcon.sprite = newSprite;
        }
    }
}