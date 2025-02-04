using Game.Runtime.Presentation.Items;
using UnityEngine;

namespace Game.Runtime.Application.Game
{
    public class GameplayRootService : MonoBehaviour, IGameplayRootService
    {
        [SerializeField] 
        private ItemsContainer _ItemsContainer;

        public void Initialize(IItemsContainerPresenter ItemsContainerPresenter)
        {
            _ItemsContainer.SetPresenter(ItemsContainerPresenter);
        }
    }
}