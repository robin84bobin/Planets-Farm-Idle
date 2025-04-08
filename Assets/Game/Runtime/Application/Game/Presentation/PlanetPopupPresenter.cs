using Game.Runtime.Application.Game.Controllers;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Presentation.Items;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Application.Game.Presentation
{
    public class PlanetPopupPresenter : IItemPopupPresenter
    {
        public string ItemId => _item.Id;
        public ReactiveProperty<string> UpgradePriceText { get; }
        public ReactiveProperty<string> PopulationValueText { get; }
        public ReactiveProperty<Sprite> UpgradePriceResourceSprite { get; private set; }
        public ReactiveProperty<Sprite> UpgradeIncomeResourceSprite { get; }

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

            UpgradePriceText = new(_item.UnlockPriceValue.ToString());
            PopulationValueText = new(_item.Population.ToString());
            UpgradePriceResourceSprite = new(_spritesConfigService.GetSprite(_item.UpgradeResourceId));
            UpgradeIncomeResourceSprite = new(_spritesConfigService.GetSprite(_item.IncomeResourceId));
        }

        public Sprite GetMainSprite()
        {
            var spriteId = _item.State == ItemState.Locked ? _item.LockedIconSpriteId : _item.IconSpriteId;
            return _spritesConfigService.GetSprite(spriteId);
        }

        public Sprite GetIncomeResourceSprite() => _spritesConfigService.GetSprite(_item.IncomeResourceId);

        public Sprite GetUpgradePriceResourceSprite()=> _spritesConfigService.GetSprite(_item.UpgradeResourceId);

        public void OnUpgradeClick() => _playerItemsController.TryUpgradeItem(_item);

        public string GetHeaderText() => _item.Id;

        public void Dispose()
        {
            UpgradePriceText.Dispose();
            PopulationValueText.Dispose();
            UpgradePriceResourceSprite.Dispose();
            UpgradeIncomeResourceSprite.Dispose();
        }
    }
}