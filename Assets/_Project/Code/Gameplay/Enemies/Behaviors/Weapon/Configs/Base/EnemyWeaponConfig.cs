using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    public abstract class EnemyWeaponConfig : ScriptableObject
    {
        public float Cooldown = 1f;
        public AttackConfig AttackConfig;

        public virtual WeaponContext CreateContext() => new();
    }
}
