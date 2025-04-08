using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Presentation.TopPanel;
using R3;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Application.Game.Presentation
{
    public class TopPanelPresenter : ITopPanelPresenter
    {
        public Sprite SoftCurrencySprite { get; }
        public ReactiveProperty<string> SoftCurrencyValueText { get; }

        private readonly PlayerResourcesController _playerResourcesController;

        [Preserve]
        public TopPanelPresenter(PlayerResourcesController playerResourcesController, IConfigsService configsService,
            ISpritesConfigService spritesConfigService)
        {
            _playerResourcesController = playerResourcesController;

            var resourceConfig = configsService.Get<ResourcesConfigs>().GetResourceConfig(Constants.Resources.SoftCurrency);
            SoftCurrencySprite = spritesConfigService.GetSprite(resourceConfig.IconName);

            var softCurrencyAmount = _playerResourcesController.PlayerResources.GetCount(Constants.Resources.SoftCurrency);
            SoftCurrencyValueText = new(softCurrencyAmount.ToString());

            _playerResourcesController.PlayerResources.ResourceCountAdded += OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved += OnResourceCountChanged;
        }

        private void OnResourceCountChanged(string resourceId, ulong changedCount, ulong totalCount)
        {
            if (resourceId == Constants.Resources.SoftCurrency)
            {
                SoftCurrencyValueText.Value = totalCount.ToString();
            }
        }

        public void Dispose()
        {
            SoftCurrencyValueText.Dispose();
            _playerResourcesController.PlayerResources.ResourceCountAdded -= OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved -= OnResourceCountChanged;
        }
    }
}