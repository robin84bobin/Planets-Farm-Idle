using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Presentation.Items
{
    public class ItemsContainer : MonoBehaviour
    {
        [SerializeField] 
        private List<ItemView> _items;
        private IItemsContainerPresenter _presenter;

        public void SetPresenter(IItemsContainerPresenter presenter)
        {
            _presenter = presenter;
            
            HideAll();
            EnableItemsViews();
        }

        private void HideAll()
        {
            foreach (var item in _items)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void EnableItemsViews()
        {
            foreach (var itemPresenter in _presenter.ItemsPresenters)
            {
                var itemView = _items.Find(i => i.Id == itemPresenter.ItemId);
                if (itemView == null)
                {
                    Debug.Log($"item view not found by id={itemPresenter.ItemId} in {this.name}");
                    continue;
                }

                itemView.gameObject.SetActive(true);
                itemView.SetPresenter(itemPresenter);
            }
        }
    }
}