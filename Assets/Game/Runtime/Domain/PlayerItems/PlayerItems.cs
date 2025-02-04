using System;
using System.Collections.Generic;
using Game.Runtime.Domain.Common;
using UnityEngine;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public class PlayerItems : ISnapshotable<PlayerItemsSnapshot>
    {
        public event Action<string, bool> ItemUnlocked;
        public Dictionary<string, Item> Items { get; private set; } = new();

        public void Add(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                if (!Items.TryAdd(item.Id, item))
                {
                    Debug.LogError($"Fail to add item id={item.Id}, because it already exists in {this}");
                }
            }
        }

        public void Update(Item item)
        {
            if (Items.ContainsKey(item.Id))
            {
                Items[item.Id] = item;
            }
            else
            {
                Debug.LogError($"Fail to update item id={item.Id}, because it does not exist in {this}");
            }
        }
        
        public PlayerItemsSnapshot GetSnapshot()
        {
            return new PlayerItemsSnapshot()
            {
                Items = Items
            };
        }

        public void RestoreFromSnapshot(PlayerItemsSnapshot snapshot)
        {
            Items = snapshot.Items;
        }

        public void SetUnlocked(Item item)
        {
            item.SetUnlocked();
            Update(item);
            
        }

        public void OnTick()
        {
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