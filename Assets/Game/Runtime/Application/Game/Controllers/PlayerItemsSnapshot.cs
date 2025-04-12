using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public struct PlayerItemsSnapshot
    {
        [JsonProperty]
        public Dictionary<string, Item> Items { get; private set; }

        public PlayerItemsSnapshot(IReadOnlyDictionary<string,Item> items)
        {
            Items = new Dictionary<string, Item>(items);
        }
    }
}