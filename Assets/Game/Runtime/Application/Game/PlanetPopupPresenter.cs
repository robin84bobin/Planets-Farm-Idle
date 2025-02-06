using System;
using Cysharp.Threading.Tasks;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Presentation.Items;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Application.Game
{
    public class PlanetPopupPresenter : IItemPopupPresenter
    {
        private readonly Item _item;
        private readonly PlayerItemsController _playerItemsController;
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;
        private readonly IPanelsService _panelsService;
        private readonly IIocFactory _iocFactory;

        [Preserve]
        public PlanetPopupPresenter(Item item, PlayerItemsController playerItemsController, 
            IConfigsService configsService, ISpritesConfigService spritesConfigService, IPanelsService panelsService,
            IIocFactory iocFactory)
        {
            _item = item;
            _playerItemsController = playerItemsController;
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
            _panelsService = panelsService;
            _iocFactory = iocFactory;
        }
        
        public void Dispose()
        {
        }

        public string ItemId { get; }
        
        AsyncReactiveProperty<string> IItemPopupPresenter.UpgradePriceText => _upgradePriceText;
        private readonly AsyncReactiveProperty<string> _upgradePriceText;
        
        public Sprite GetMainSprite()
        {
            throw new NotImplementedException();
        }

        public Sprite GetRewardResourceSprite()
        {
            throw new NotImplementedException();
        }

        public Sprite GetUpgradePriceResourceSprite()
        {
            throw new NotImplementedException();
        }

        public void OnUpgradeClick()
        {
            throw new NotImplementedException();
        }
    }
}