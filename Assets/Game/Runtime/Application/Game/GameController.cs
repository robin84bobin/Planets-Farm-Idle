using System;
using Game.Runtime.Application.Resources;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Presentation.Items;
using Game.Runtime.Presentation.TopPanel;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace Game.Runtime.Application.Game
{
    public class GameController : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly PlayerItemsController _playerItemsController;
        private readonly ISavesController _gameSaveController;
        private readonly IIocFactory _iocFactory;
        private readonly IPanelsService _panelsService;
        private readonly IGameplayRootService _gameplayRootService; 
            

        [Preserve]
        public GameController(PlayerResourcesController playerResourcesController, 
            PlayerItemsController playerItemsController, ISavesController gameSaveController,
            IIocFactory iocFactory, IPanelsService panelsService, IGameplayRootService gameplayRootService)
        {
            _playerResourcesController = playerResourcesController;
            _playerItemsController = playerItemsController;
            _gameSaveController = gameSaveController;
            _iocFactory = iocFactory;
            _panelsService = panelsService;
            _gameplayRootService = gameplayRootService;
        }

        void IInitializable.Initialize()
        {
            _playerResourcesController.Initialize();
            _playerItemsController.Initialize();

            _panelsService.Open<TopPanel>()
                .SetPresenter(_iocFactory.Create<TopPanelPresenter>());

            _gameplayRootService.Initialize(_iocFactory.Create<ItemsContainerPresenter>());
            
            _gameSaveController.SaveAllLocal();
        }

        public void Tick()
        {
            _playerItemsController.OnTick();
        }

        public void Dispose()
        {
        }
    }
}