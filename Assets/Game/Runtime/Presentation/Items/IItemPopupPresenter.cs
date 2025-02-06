using System;
using R3;
using UnityEngine;

namespace Game.Runtime.Presentation.Items
{
    public interface IItemPopupPresenter : IDisposable
    {
        string ItemId { get; }
        Sprite GetMainSprite();
        ReactiveProperty<Sprite> UpgradeIncomeResourceSprite { get; }
        ReactiveProperty<Sprite> UpgradePriceResourceSprite { get; }
        ReactiveProperty<string> UpgradePriceText { get; }
        ReactiveProperty<string> PopulationValueText { get; }
        void OnUpgradeClick();
        string GetHeaderText();
    }
}