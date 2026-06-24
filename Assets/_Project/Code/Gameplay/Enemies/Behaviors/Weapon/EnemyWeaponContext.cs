using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    public sealed class EnemyWeaponContext
    {
        public EnemyAgent Agent;
        public EnemyWeaponConfig Config;
        public WeaponContext WeaponContext;
        public Vector3 PlayerPosition;
        public ICombatEntity PlayerTarget;
        public float DeltaTime;
    }
}
