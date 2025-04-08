using System;
using R3;
using UnityEngine;

namespace Game.Runtime.Presentation.Items
{
    public interface IItemViewPresenter : IDisposable
    {
        string ItemId { get; }
        Sprite GetMainSprite();
        
        ReactiveProperty<bool> IsLockedState { get; }
        ReactiveProperty<bool> IsProgressState { get; }
        ReactiveProperty<bool> IsRewardedState { get; }
        ReactiveProperty<float> IncomeProgress { get; }
        string IncomeProgressBarText { get; }
        Sprite GetIncomeResourceSprite();
        Sprite GetUnlockResourceSprite();
        string GetUnlockPriceText();
        void OnItemClick();
        void OnIncomeClick();
    }
}