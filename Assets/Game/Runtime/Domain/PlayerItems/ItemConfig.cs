using System;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public struct ItemConfig
    {
        public string Id;
        public string IconSpriteId;
        public string LockedIconSpriteId;

        public string PriceResourceId;
        public ulong PriceValue;
    }
}