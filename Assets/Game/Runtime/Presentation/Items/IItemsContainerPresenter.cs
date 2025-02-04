using System.Collections.Generic;

namespace Game.Runtime.Presentation.Items
{
    public interface IItemsContainerPresenter
    {
        public List<IItemViewPresenter> ItemsPresenters { get; }
    }
}