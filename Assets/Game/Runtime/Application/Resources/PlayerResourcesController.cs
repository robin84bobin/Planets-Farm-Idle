using System;
using Game.Runtime.Domain.Common;
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
        

        [Preserve]
        public PlayerResourcesController(IRepositoryService repositoryService, IConfigsService configsService)
        {
            _repositoryService = repositoryService;
            _configsService = configsService;
        }

        public void Save()
        {
            _repositoryService.Save(PlayerResources.GetSnapshot());
        }

        public void Initialize()
        {
            PlayerResources = new PlayerResources();
            if (_repositoryService.TryLoad<PlayerResourcesSnapshot>(out var snapshot))
            {
                PlayerResources.RestoreFromSnapshot(snapshot);
                return;
            }

            var softCurrencyConfig = _configsService.Get<ResourcesConfigs>().GetResourceConfig(Constants.Resources.SoftCurrency);
            PlayerResources.Add(new Resource(Constants.Resources.SoftCurrency, softCurrencyConfig.DefaultValue));
        }
    }
}
