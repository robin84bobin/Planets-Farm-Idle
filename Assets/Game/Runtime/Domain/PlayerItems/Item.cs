using System;
using UnityEngine.Serialization;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public class Item
    {
        public event Action Unlocked;
        public event Action Upgraded;
        public event Action<float> ItemIncomeProgress;
        public string Id;
        public int Level;
        public int MaxLevel;
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
        public ulong UpdradePriceValue;
        public ItemState State = ItemState.Locked;

        public float GetIncomeProgress() => 0;

        public void SetUnlocked(DateTime time)
        {
            UpgradeLevel();
            Unlocked?.Invoke();
        }

        public void UpgradeLevel()
        {
            Level++;
            State = ItemState.InProgress;
            // todo UpdateNextIncomeTime();
            
            Upgraded?.Invoke();
        }

        private void UpdateNextIncomeTime()
        {
            
        }
    }
}