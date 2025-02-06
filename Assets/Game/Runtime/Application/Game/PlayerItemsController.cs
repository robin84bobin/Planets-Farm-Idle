using System;
using Game.Runtime.Domain.PlayerItems;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Resources
{
    public class PlayerItemsController : ISaveable
    {
        public event Action<string, bool> ItemUnlocked;
        public PlayerItems PlayerItems { get; private set; }

        private readonly IRepositoryService _repositoryService;
        private readonly IConfigsService _configsService;
        private readonly PlayerResourcesController _playerResourcesController;

        public void Save()
        {
            _repositoryService.Save(PlayerItems.GetSnapshot());
        }

        [Preserve]
        public PlayerItemsController(IRepositoryService repositoryService, IConfigsService configsService, 
            PlayerResourcesController playerResourcesController)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
            _playerResourcesController = playerResourcesController;
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
            
            var resourceId = item.GetUnlockPriceResourceId();
            var value = item.GetUnlockPriceValue();

            var resource = new Resource(resourceId, value);
            if (!_playerResourcesController.PlayerResources.HasEnough(resource))
            {
                ItemUnlocked?.Invoke(itemId, false);
                return;
            }
            
            _playerResourcesController.PlayerResources.Remove(resource);
            item.SetUnlocked();
            ItemUnlocked?.Invoke(itemId, true);
        }

        public void TryUpgradeItem(Item item)
        {
            var resourceId = item.GetUpgradePriceResourceId();
            var value = item.GetUpgradePriceValue();

            var resource = new Resource(resourceId, value);
            if (_playerResourcesController.PlayerResources.HasEnough(resource))
            {
                _playerResourcesController.PlayerResources.Remove(resource);
                item.SetUpgraded();
            }
        }
    }
}