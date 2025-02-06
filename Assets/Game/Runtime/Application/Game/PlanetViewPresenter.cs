using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Presentation.Items;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Application.Game
{
    public class PlanetViewPresenter : IItemViewPresenter
    {
        public string ItemId => _itemId;
        public Sprite GetMainSprite() => IsLockedState.Value ? _lockedIconSprite : _iconSprite;
        public ReactiveProperty<bool> IsLockedState { get; private set; }
        public ReactiveProperty<bool> IsProgressState { get; private set; }
        public ReactiveProperty<bool> IsRewardedState { get; private set; }
        public Sprite GetUnlockResourceSprite() => _unlockPriceResourceSprite;

        public ReactiveProperty<float> IncomeProgress { get; private set; }

        private readonly PlayerItemsController _playerItemsController;
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;
        private readonly IPanelsService _panelsService;
        private readonly IIocFactory _iocFactory;
        private readonly Item _item;
        private string _itemId;
        private Sprite _lockedIconSprite;
        private Sprite _iconSprite;
        private Sprite _unlockPriceResourceSprite;


        [Preserve]
        public PlanetViewPresenter(Item item, PlayerItemsController playerItemsController, 
            IConfigsService configsService, ISpritesConfigService spritesConfigService, IPanelsService panelsService,
            IIocFactory iocFactory)
        {
            _item = item;
            _playerItemsController = playerItemsController;
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
            _panelsService = panelsService;
            _iocFactory = iocFactory;

            Initialize();
        }

        private void Initialize()
        {
            _itemId = _item.Id;
            _iconSprite = _spritesConfigService.GetSprite(_item.IconSpriteId);
            _lockedIconSprite = _spritesConfigService.GetSprite(_item.LockedIconSpriteId);
            _unlockPriceResourceSprite = _spritesConfigService.GetSprite(_item.UnlockPriceResourceId);
            IsLockedState = new(_item.State == ItemState.Locked);
            IsProgressState = new(_item.State == ItemState.InProgress);
            IncomeProgress = new(_item.GetIncomeProgress());

            _item.Unlocked += OnUnlocked;
        }

        public void Dispose()
        {
            _item.Unlocked -= OnUnlocked;
            IsLockedState.Dispose();
            IncomeProgress.Dispose();
        }

        public void OnItemClick()
        {
            if (IsLockedState.Value)
            {
                _playerItemsController.TryUnlockItem(_itemId);
            }
            else
            {
                _panelsService.Open<ItemPopupPanel>()
                    .SetPresenter(_iocFactory.Create<PlanetPopupPresenter>(_item));
            }
        }

        private void OnUnlocked() => IsLockedState.Value = _item.State == ItemState.Locked;
        public void OnIncomeClick() => _playerItemsController.TryGetRewardFromItem(_itemId);
        public string GetUnlockPriceText() => _item.UnlockPriceValue.ToString();
        public Sprite GetIncomeResourceSprite() => _spritesConfigService.GetSprite(_item.IncomeResourceId);

    }
}