using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Enemies.Behaviors.Movement;
using _Project.Code.Gameplay.Enemies.Behaviors.Weapon;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Enemy Config")]
    public sealed class EnemyConfig : ScriptableObject
    {
        public StatsConfig StatsConfig;
        public List<DefenseModifierConfig> DefenseModifiers = new();
        public EnemyView Prefab;

        [Space]
        public EnemyMovementBehaviorConfig MovementBehavior;
        public EnemyWeaponConfig Weapon;
    }
}
