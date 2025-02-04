using System;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Presentation.InfoPopup;
using Game.Runtime.Presentation.Items;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Application.Game
{
    public class PlanetViewViewPresenter : IItemViewPresenter
    {
        public string ItemId => _itemId;
        public Sprite GetMainSprite() => IsLocked.Value ? _lockedIconSprite : _iconSprite;
        public Sprite GetUnlockResourceSprite() => _unlockPriceResourceSprite;

        public ReactiveProperty<bool> IsLocked { get; } 
        public ReactiveProperty<float> IncomeProgress { get; }

        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;
        private readonly IIocFactory _iocFactory;
        private readonly string _itemId;
        private Sprite _lockedIconSprite;
        private Sprite _iconSprite;
        private PanelsService _panelsService;
        private Sprite _unlockPriceResourceSprite;
        private readonly Item _item;


        [Preserve]
        public PlanetViewViewPresenter(Item item, PlayerResourcesController playerResourcesController, 
            IConfigsService configsService, ISpritesConfigService spritesConfigService, PanelsService panelsService,
            IIocFactory iocFactory)
        {
            _playerResourcesController = playerResourcesController;
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
            _item = item;
        }

        void IDisposable.Dispose()
        {
            //throw new System.NotImplementedException();
        }

        public void OnItemClick()
        {
            if (IsLocked.Value)
            {
                bool result = _playerResourcesController.TryUnlockItem(_itemId);
                if (!result)
                {
                    _panelsService.Open<InfoPopupPanel>()
                        .SetPresenter(_iocFactory.Create<InfoPopupPresenter>("fail to buy item"));
                }

                IsLocked.Value = !result;  
            }
            else
            {
                _panelsService.Open<ItemPopupPanel>()
                    .SetPresenter(_iocFactory.Create<IItemPopupPresenter>(_itemId));
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