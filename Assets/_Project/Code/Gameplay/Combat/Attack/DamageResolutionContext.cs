using System.Collections.Generic;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class DamageResolutionContext
    {
        public ICombatEntity Source;
        public AttackConfig AttackConfig;
        public float Damage;
        public DamageType DamageType;
        public DamageTags DamageTags;
        public IReadOnlyList<OnHitModifierConfig> OnHitModifiers;

        public void Reset(ICombatEntity source, AttackConfig attackConfig)
        {
            Source = source;
            AttackConfig = attackConfig;
            Damage = attackConfig.BaseDamage;
            DamageType = attackConfig.DamageType;
            DamageTags = attackConfig.DamageTags;
            OnHitModifiers = attackConfig.OnHitModifiers;
        }
    }
}
