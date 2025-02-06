using System;
using Game.Runtime.Application.Resources;
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
        public Sprite GetMainSprite() => IsLocked.Value ? _lockedIconSprite : _iconSprite;
        public Sprite GetUnlockResourceSprite() => _unlockPriceResourceSprite;

        public ReactiveProperty<bool> IsLocked { get; } 
        public ReactiveProperty<float> IncomeProgress { get; }

        private readonly PlayerItemsController _playerItemsController;
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;
        private readonly IPanelsService _panelsService;
        private readonly IIocFactory _iocFactory;
        private readonly Item _item;
        private readonly string _itemId;
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

            _itemId = item.Id;
            _iconSprite = _spritesConfigService.GetSprite(item.IconSpriteId);
            _lockedIconSprite = _spritesConfigService.GetSprite(item.LockedIconSpriteId);
            _unlockPriceResourceSprite = _spritesConfigService.GetSprite(item.UnlockPriceResourceId);
            IsLocked = new (item.IsLocked);
            IncomeProgress = new (item.GetIncomeProgress());

            _item.Unlocked += OnUnlocked;
        }

        private void OnUnlocked() => IsLocked.Value = _item.IsLocked;

        void IDisposable.Dispose()
        {
            _item.Unlocked -= OnUnlocked;
        }

        public void OnItemClick()
        {
            if (IsLocked.Value)
            {
                _playerItemsController.TryUnlockItem(_itemId);
            }
            else
            {
                _panelsService.Open<ItemPopupPanel>()
                    .SetPresenter(_iocFactory.Create<PlanetPopupPresenter>(_itemId));
            }
        }

        public void OnRewardClick()
        {
            throw new NotImplementedException();
        }

        public string GetUnlockPriceText() => _item.UnlockPriceValue.ToString();
        public Sprite GetRewardResourceSprite() => _spritesConfigService.GetSprite(_item.UnlockPriceResourceId);

    }
}