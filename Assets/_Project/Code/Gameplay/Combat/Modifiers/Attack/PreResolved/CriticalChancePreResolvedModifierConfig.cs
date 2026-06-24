using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "TDS/Combat/Attack PreResolved Modifiers/Critical Chance")]
    public sealed class CriticalChancePreResolvedModifierConfig : PreResolvedModifierConfig
    {
        [Range(0f, 1f)] public float CritChance = 0.1f;
        public float CritMultiplier = 2f;
    }
    
    public sealed class CriticalChancePreResolvedModifierProcessor : IPreResolvedModifierProcessor
    {
        public Type ConfigType => typeof(CriticalChancePreResolvedModifierConfig);

        public void Apply(PreResolvedModifierConfig config, DamageResolutionContext context)
        {
            var crit = (CriticalChancePreResolvedModifierConfig)config;
            if (Random.value > crit.CritChance) return;

            context.Damage *= crit.CritMultiplier;
            context.DamageTags |= DamageTags.Critical;
        }
    }
}
