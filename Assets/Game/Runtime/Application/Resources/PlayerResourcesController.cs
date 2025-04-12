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
            _repositoryService.Save(new PlayerResourcesSnapshot(PlayerResources.Resources));
        }

        public void Initialize()
        {
            PlayerResources = new PlayerResources();
            if (_repositoryService.TryLoad<PlayerResourcesSnapshot>(out var snapshot))
            {
                PlayerResources.Update(snapshot.Resources);
                return;
            }

            var softCurrencyConfig = _configsService.Get<ResourcesConfigs>().GetResourceConfig(Constants.Resources.SoftCurrency);
            PlayerResources.Add(new Resource(Constants.Resources.SoftCurrency, softCurrencyConfig.DefaultValue));
        }

        public ulong GetResourceCount(string softCurrency) => PlayerResources.GetCount(softCurrency);
    }
}
