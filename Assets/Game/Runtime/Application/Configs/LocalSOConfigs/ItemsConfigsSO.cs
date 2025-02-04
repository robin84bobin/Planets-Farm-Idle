using Game.Runtime.Domain.PlayerItems;
using Game.Runtime.Domain.PlayerResources;
using UnityEngine;

namespace Game.Runtime.Application.Configs.LocalSOConfigs
{
    [CreateAssetMenu(fileName = nameof(ItemsConfigsSO), menuName = "Game/Configs/" + nameof(ItemsConfigsSO),
        order = 0)]
    public class ItemsConfigsSO : ScriptableObject
    {
        public ItemsConfigs ItemsConfigs;
        
    }
}