using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    public sealed class WeaponContext
    {
        public ICombatEntity Source;
        public Vector3 Origin;
        public Vector3 AimPoint;
        public Vector3 AimDirection;
        public Vector3 MuzzlePosition;
    }
}
