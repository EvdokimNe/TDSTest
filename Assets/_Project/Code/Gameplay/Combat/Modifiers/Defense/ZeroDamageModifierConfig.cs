using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "TDS/Combat/Defense Modifiers/Zero Damage")]
    public sealed class ZeroDamageModifierConfig : DefenseModifierConfig
    {
        [Range(0f, 1f)] public float Chance = 0.1f;

        public override string GetDescription() =>
            $"Уклонение: {Chance * 100f:0}% шанс полностью избежать урон";
    }
    
    public sealed class ZeroDamageModifierProcessor : IDefenseModifierProcessor
    {
        public Type ConfigType => typeof(ZeroDamageModifierConfig);

        public void Apply(DefenseModifierConfig config, DamageContext context)
        {
            var zeroDamageConfig = (ZeroDamageModifierConfig)config;
            if (Random.value > zeroDamageConfig.Chance) return;

            context.CurrentDamage = 0f;
        }
    }
}
