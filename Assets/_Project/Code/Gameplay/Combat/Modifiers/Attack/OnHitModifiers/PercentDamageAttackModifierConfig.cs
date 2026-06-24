using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "TDS/Combat/Attack Modifiers/Percent Damage")]
    public sealed class PercentDamageOnHitModifierConfig : OnHitModifierConfig
    {
        public float Percent;
    }
    
    public sealed class PercentDamageOnHitModifierProcessor : IOnHitModifierProcessor
    {
        public Type ConfigType => typeof(PercentDamageOnHitModifierConfig);

        public void Apply(OnHitModifierConfig config, DamageContext context)
        {
            var percentConfig = (PercentDamageOnHitModifierConfig)config;
            context.CurrentDamage *= 1f + percentConfig.Percent;
        }
    }
}
