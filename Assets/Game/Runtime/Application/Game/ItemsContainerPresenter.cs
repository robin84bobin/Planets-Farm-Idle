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
        
        private readonly PlayerItemsController _playerItemsController;
        private readonly IIocFactory _iocFactory;

        [Preserve]
        public ItemsContainerPresenter(PlayerItemsController playerItemsController,
            IIocFactory iocFactory)
        {
            _playerItemsController = playerItemsController;
            _iocFactory = iocFactory;

            InitializeItems();
        }

        private void InitializeItems()
        {
            var items = _playerItemsController.PlayerItems.Items;
            ItemsPresenters = new List<IItemViewPresenter>(items.Count);
            
            foreach (var item in items)
            {
                IItemViewPresenter newItemView = _iocFactory.Create<PlanetViewPresenter>(item.Value);
                ItemsPresenters.Add(newItemView);
            }
        }
    }
}