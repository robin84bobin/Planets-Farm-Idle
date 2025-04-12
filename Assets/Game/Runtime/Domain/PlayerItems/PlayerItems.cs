using System;
using System.Collections.Generic;
using Game.Runtime.Domain.PlayerResources;
using UnityEngine;

namespace Game.Runtime.Domain.PlayerItems
{
    [Serializable]
    public class PlayerItems //: ISnapshotable<PlayerItemsSnapshot>
    {
        public IReadOnlyDictionary<string, Item> Items => _items;
        private Dictionary<string, Item> _items = new();

        public void Add(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                if (!_items.TryAdd(item.Id, item))
                {
                    Debug.LogError($"Fail to add item id={item.Id} :  already exists");
                }
            }
        }

        public void OnTick()
        {
        }

        public void Update(Dictionary<string,Item> items)
        {
            _items = items;
        }

        public Item GetItem(string itemId)
        {
            if (Items.TryGetValue(itemId, out var item))
            {
                return item;
            }

            throw new Exception($"Item Id not exists in {this}");
        }
    }
}