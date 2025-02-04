using System;
using R3;
using UnityEngine;

namespace Game.Runtime.Presentation.Items
{
    public interface IItemViewPresenter : IDisposable
    {
        string ItemId { get; }
        Sprite GetMainSprite();
        
        ReactiveProperty<bool> IsLocked { get; }
        ReactiveProperty<float> IncomeProgress { get; }
        Sprite GetRewardResourceSprite();
        Sprite GetUnlockResourceSprite();
        string GetUnlockPriceText();
        void OnItemClick();
        void OnRewardClick();
    }
}