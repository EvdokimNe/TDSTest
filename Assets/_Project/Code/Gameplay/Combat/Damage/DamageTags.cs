using System;

namespace _Project.Code.Gameplay.Combat
{
    [Flags]
    public enum DamageTags
    {
        None = 0,
        Critical = 1 << 0,
        Projectile = 1 << 1,
        Melee = 1 << 2,
        Ability = 1 << 3,
        Area = 1 << 4,
        DamageOverTime = 1 << 5
    }
}
