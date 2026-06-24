using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using UnityEngine;
namespace _Project.Code.Gameplay.Player
{
    [CreateAssetMenu(menuName = "TDS/Player/Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        public StatsConfig StatsConfig;
        public List<DefenseModifierConfig> DefenseModifiers = new();
    }
}
