using System;
using System.Collections.Generic;

namespace Game.Runtime.Presentation.Items
{
    public interface IItemsContainerPresenter : IDisposable
    {
        public List<IItemViewPresenter> ItemsPresenters { get; }
    }
}