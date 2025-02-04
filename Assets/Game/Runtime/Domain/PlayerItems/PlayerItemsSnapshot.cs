using System;
using System.Collections.Generic;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public struct PlayerItemsSnapshot
    {
        public Dictionary<string, Item> Items { get; set; }
    }
}