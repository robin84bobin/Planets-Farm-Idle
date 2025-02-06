using System;
using System.Collections.Generic;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerResources;

namespace Game.Runtime.Domain.PlayerItems
{
    [Serializable]
    public class ItemsConfigs
    {
        public List<ItemConfig> ItemConfigs;
        public List<ItemLevelConfig> ItemsLevelConfigs;

        public IEnumerable<Item> CreateAllItems()
        {
            int i = 0;

            while (i < ItemConfigs.Count)
            {
                var configItem = ItemConfigs[i];
                yield return new Item()
                {
                    Id = configItem.Id,
                    IconSpriteId = configItem.IconSpriteId,
                    LockedIconSpriteId = configItem.LockedIconSpriteId,
                    UnlockPriceResourceId = configItem.PriceResourceId,
                    UnlockPriceValue = configItem.PriceValue
                };
                i++;
            }
        }
        
        public IEnumerable<Item> GetMockItems(int count)
        {
            int i = 1;

            while (i <= count)
            {
                var id = $"Planet{i}";
                yield return new Item()
                {
                    Id = id,
                    Level = 0,
                    Population = 1000,
                    IconSpriteId = id,
                    LockedIconSpriteId = id + Constants.Suffix.LockedSprite,
                    UnlockPriceResourceId = Constants.Resources.SoftCurrency,
                    UnlockPriceValue = 10,
                    IncomeResourceId = Constants.Resources.SoftCurrency,
                    IncomeValue = 50,
                    IncomePeriodSec = 10,
                    UpgradeResourceId = Constants.Resources.SoftCurrency,
                    UpdradePriceValue = 100
                };
                i++;
            }
                
        }
        
        public ItemConfig GetResourceConfig(string itemId)
        {
            foreach (var config in ItemConfigs)
            {
                if (config.Id == itemId)
                {
                    return config;
                }
            }

            throw new ArgumentException($"Config for resource id {itemId} is not exists");
        }
    }

    public class ItemLevelConfig
    {
    }
}