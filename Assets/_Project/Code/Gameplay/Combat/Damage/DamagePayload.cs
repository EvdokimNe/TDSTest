using System.Collections.Generic;

namespace _Project.Code.Gameplay.Combat
{
    public readonly struct DamagePayload
    {
        public readonly ICombatEntity Source;
        public readonly float Damage;
        public readonly DamageType Type;
        public readonly DamageTags Tags;
        public readonly IReadOnlyList<OnHitModifierConfig> OnHitModifiers;

        public DamagePayload(
            ICombatEntity source,
            float damage,
            DamageType type,
            DamageTags tags,
            IReadOnlyList<OnHitModifierConfig> onHitModifiers)
        {
            Source = source;
            Damage = damage;
            Type = type;
            Tags = tags;
            OnHitModifiers = onHitModifiers;
        }

        public DamageRequest CreateRequest(ICombatEntity target)
        {
            return new DamageRequest(this, target);
        }
    }
}
