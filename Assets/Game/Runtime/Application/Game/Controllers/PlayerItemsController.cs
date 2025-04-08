using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.PlayerItems;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Infrastructure.Time;
using Game.Runtime.Presentation.InfoPopup;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Game.Controllers
{
    public class PlayerItemsController : ISaveable
    {
        public PlayerItems PlayerItems { get; private set; }

        private readonly IRepositoryService _repositoryService;
        private readonly IConfigsService _configsService;
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IPanelsService _panelsService;
        private readonly IIocFactory _iocFactory;
        private readonly ITimeService _timeService;

        public void Save()
        {
            _repositoryService.Save(PlayerItems.GetSnapshot());
        }

        [Preserve]
        public PlayerItemsController(IRepositoryService repositoryService, IConfigsService configsService, 
            PlayerResourcesController playerResourcesController, IPanelsService panelsService, IIocFactory iocFactory,
            ITimeService timeService)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
            _playerResourcesController = playerResourcesController;
            _panelsService = panelsService;
            _iocFactory = iocFactory;
            _timeService = timeService;
        }
        
        public void Initialize()
        {
            PlayerItems = new PlayerItems();
            if (_repositoryService.TryLoad<PlayerItemsSnapshot>(out var snapshot))
            {
                PlayerItems.RestoreFromSnapshot(snapshot);
                return;
            }
        
            var farmItemsConfigs = _configsService.Get<ItemsConfigs>();
            var items = farmItemsConfigs.GetMockItems(9);
            PlayerItems.Add(items);
        }
        
        public void TryUnlockItem(in string itemId)
        {
            var item = PlayerItems.GetItem(itemId);
            var resourceId = item.UnlockPriceResourceId;
            var value = item.UnlockPriceValue;

            var resource = new Resource(resourceId, value);
            if (!_playerResourcesController.PlayerResources.HasEnough(resource))
            {
                OnItemUnlocked(itemId, false);
                return;
            }
            
            _playerResourcesController.PlayerResources.Remove(resource);
            item.SetUnlocked(_timeService.CurrentTime);
            OnItemUnlocked(itemId, true);
        }

        public void TryUpgradeItem(Item item)
        {
            var resourceId = item.UpgradeResourceId;
            var value = item.UpdradePriceValue;

            var resource = new Resource(resourceId, value);
            if (_playerResourcesController.PlayerResources.HasEnough(resource))
            {
                _playerResourcesController.PlayerResources.Remove(resource);
                item.UpgradeLevel();
            }
        }

        private void OnItemUnlocked(string itemId, bool success)
        {
            if (!success)
            {
                _panelsService.Open<InfoPopupPanel>()
                    .SetPresenter(_iocFactory.Create<InfoPopupPresenter>($"fail to unlock item {itemId}"));
            }
        }
        
        public void TryGetRewardFromItem(string itemId)
        {
            if (PlayerItems.Items.TryGetValue(itemId, out var item))
            {
                if (item.incomeProgress < 1f)
                    return;
                item.GrabReward();
            }
        }

        public void OnTick()
        {
            foreach (var item in PlayerItems.Items)
            {
                item.Value.UpdateTime(_timeService.CurrentTime);
            }
        }
    }
}