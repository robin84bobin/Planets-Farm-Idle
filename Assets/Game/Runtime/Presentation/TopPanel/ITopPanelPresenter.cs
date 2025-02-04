using System;
using R3;
using UnityEngine;

namespace Game.Runtime.Presentation.TopPanel
{
    public interface ITopPanelPresenter : IDisposable
    {
        Sprite SoftCurrencySprite { get; }
        ReactiveProperty<string> SoftCurrencyValueText { get; }
    }
}