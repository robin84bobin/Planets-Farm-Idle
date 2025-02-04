using System;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public class Item
    {
        public string Id;
        public int Level;
        public int RewardTime;
        
        public string IconSpriteId;
        public string LockedIconSpriteId;

        public int Population;
        
        public string IncomeResourceId;
        public ulong IncomeValue;
        public int IncomePeriodSec;

        public string UnlockPriceResourceId;
        public ulong UnlockPriceValue;
        
        public string UpgradeResourceId;
        public ulong UpdragePriceValue;
        public bool IsLocked => Level <= 0;
        
        public ulong GetUnlockPriceValue() => UnlockPriceValue;
        public string GetUnlockPriceResourceId() => UnlockPriceResourceId;
        public void SetUnlocked() => Level = 1;

        public string GetUpgradePriceResourceId() => UpgradeResourceId;
        public ulong GetUpgradePriceValue() => UpdragePriceValue;
        public void SetUpgraded() => Level++;

        public float GetIncomeProgress()
        {
            throw new NotImplementedException();
        }
    }
}