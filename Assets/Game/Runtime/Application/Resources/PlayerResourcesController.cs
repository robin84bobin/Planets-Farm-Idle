using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerItems;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Resources
{
    public class PlayerResourcesController : ISaveable
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IConfigsService _configsService;

        public PlayerResources PlayerResources { get; private set; }
        public PlayerItems PlayerItems { get; private set; }

        [Preserve]
        public PlayerResourcesController(IRepositoryService repositoryService, IConfigsService configsService)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
        }

        public void Save()
        {
            _repositoryService.Save(PlayerResources.GetSnapshot());
            _repositoryService.Save(PlayerItems.GetSnapshot());
        }

        public void Initialize()
        {
            InitializeResources();
            InitializeFarmItems();
        }

        private void InitializeResources()
        {
            PlayerResources = new PlayerResources();
            if (_repositoryService.TryLoad<PlayerResourcesSnapshot>(out var snapshot))
            {
                PlayerResources.RestoreFromSnapshot(snapshot);
                return;
            }
            
            PlayerResources.Add(new Resource(Constants.Resources.SoftCurrency, 3000));
        }

        private void InitializeFarmItems()
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

        public bool TryUnlockItem(string itemId)
        {
            var item = PlayerItems.GetItem(itemId);
            
            var resourceId = item.GetUnlockPriceResourceId();
            var value = item.GetUnlockPriceValue();

            var resource = new Resource(resourceId, value);
            if (!PlayerResources.HasEnough(resource))
            {
                return false;
            }
            
            PlayerResources.Remove(resource);
            item.SetUnlocked();
            return true;
        }

        public void TryUpgradeItem(Item item)
        {
            var resourceId = item.GetUpgradePriceResourceId();
            var value = item.GetUpgradePriceValue();

            var resource = new Resource(resourceId, value);
            if (PlayerResources.HasEnough(resource))
            {
                PlayerResources.Remove(resource);
                //PlayerFarmItems.SetUpgraded(item);
            }
        }

    }
}
