using System;
using UnityEngine.Serialization;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public class Item
    {
        public event Action StateChanged;
        public event Action Upgraded;
        public event Action<float> ItemIncomeProgressChanged;
        public string Id;
        public int Level;
        public int MaxLevel;
        public DateTime RewardTime;
        public DateTime StartTime;
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

        public float incomeProgress;
        public double incomeRemainSec;

        public void SetUnlocked(DateTime time)
        {
            UpgradeLevel();
            StateChanged?.Invoke();
        }

        public void UpgradeLevel()
        {
            Level++;
            State = ItemState.InProgress;
            // todo UpdateNextIncomeTime();
            
            Upgraded?.Invoke();
        }

        public void UpdateTime(DateTime currentTime)
        {
            if (State != ItemState.InProgress)
                return;
            
            if (RewardTime == DateTime.MinValue)
            {
                StartTime = currentTime;
                RewardTime = StartTime.AddSeconds(IncomePeriodSec);
            }

            incomeRemainSec = RewardTime.Subtract(currentTime).TotalSeconds;
            
            if (currentTime <= RewardTime)
            {
                incomeProgress = (float)(currentTime - StartTime).TotalSeconds / IncomePeriodSec;
                SetState(ItemState.InProgress);
            }
            else
            {
                incomeProgress = 1;
                SetState(ItemState.Rewarded);
            }

            ItemIncomeProgressChanged?.Invoke(incomeProgress);
        }

        private void SetState(ItemState state)
        {
            if (State == state)
                return;

            State = state;
            StateChanged?.Invoke();
        }
        
        public Tuple<string, ulong> ClaimRewards()
        {
            ResetProgress();
            return GetCurrentReward();
        }

        private void ResetProgress()
        {
            RewardTime = DateTime.MinValue;
            incomeProgress = 0;
            SetState(ItemState.InProgress);
        }

        public Tuple<string,ulong> GetCurrentReward()
        {
            return new Tuple<string, ulong>(IncomeResourceId, IncomeValue);
        }
    }
}