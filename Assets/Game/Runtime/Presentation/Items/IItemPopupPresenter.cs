using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Runtime.Presentation.Items
{
    public interface IItemPopupPresenter : IDisposable
    {
        string ItemId { get; }
        Sprite GetMainSprite();
        
        Sprite GetRewardResourceSprite();
        Sprite GetUpgradePriceResourceSprite();
        AsyncReactiveProperty<string> UpgradePriceText { get; }
        void OnUpgradeClick();
    }
}