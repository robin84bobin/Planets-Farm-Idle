using System;
using Cysharp.Threading.Tasks;
using Game.Runtime.Presentation.Items;
using UnityEngine;

namespace Game.Runtime.Application.Game
{
    public class PlanetPopupPresenter : IItemPopupPresenter
    {
        public void Dispose()
        {
        }

        public string ItemId { get; }
        
        AsyncReactiveProperty<string> IItemPopupPresenter.UpgradePriceText => _upgradePriceText;
        private readonly AsyncReactiveProperty<string> _upgradePriceText;
        
        public Sprite GetMainSprite()
        {
            throw new NotImplementedException();
        }

        public Sprite GetRewardResourceSprite()
        {
            throw new NotImplementedException();
        }

        public Sprite GetUpgradePriceResourceSprite()
        {
            throw new NotImplementedException();
        }

        public void OnUpgradeClick()
        {
            throw new NotImplementedException();
        }
    }
}