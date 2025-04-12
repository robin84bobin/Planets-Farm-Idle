using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public struct PlayerResourcesSnapshot
    {
        [JsonProperty]
        public Dictionary<string, ulong> Resources { get; private set; }
        
        public PlayerResourcesSnapshot(IReadOnlyDictionary<string,ulong> playerResourcesResources)
        {
            Resources = new Dictionary<string, ulong>(playerResourcesResources);
        }
    }
}
