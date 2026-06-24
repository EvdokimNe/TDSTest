using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    public sealed class AbilityContext
    {
        public ICombatEntity Source;
        public Vector3 Origin;
        public Vector3 AimPoint;
        public Vector3 AimDirection;
    }
}
