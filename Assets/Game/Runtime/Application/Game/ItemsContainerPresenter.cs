using System.Collections.Generic;
using Game.Runtime.Application.Resources;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Presentation.Items;
using VContainer;

namespace Game.Runtime.Application.Game
{
    public class ItemsContainerPresenter : IItemsContainerPresenter
    {
        public List<IItemViewPresenter> ItemsPresenters { get; private set; }
        
        private readonly PlayerResourcesController _playerResourcesController;
        private readonly IIocFactory _iocFactory;

        [Preserve]
        public ItemsContainerPresenter(PlayerResourcesController playerResourcesController,
            IIocFactory iocFactory)
        {
            _playerResourcesController = playerResourcesController;
            _iocFactory = iocFactory;

            InitializeItems();
        }

        private void InitializeItems()
        {
            var items = _playerResourcesController.PlayerItems.Items;
            ItemsPresenters = new List<IItemViewPresenter>(items.Count);
            
            foreach (var item in items)
            {
                IItemViewPresenter newItemView = _iocFactory.Create<PlanetViewViewPresenter>(item.Value);
                ItemsPresenters.Add(newItemView);
            }
        }
    }
}