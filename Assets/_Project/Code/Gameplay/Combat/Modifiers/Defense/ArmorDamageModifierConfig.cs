using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "TDS/Combat/Defense Modifiers/Armor")]
    public sealed class ArmorDamageModifierConfig : DefenseModifierConfig
    {
        public float ReductionPerArmor = 0.01f;
        [Range(0f, 1f)] public float MaxReduction = 0.75f;

        public override string GetDescription() =>
            $"Броня: {ReductionPerArmor * 100f:0.#}% физ. урона за единицу брони (макс. {MaxReduction * 100f:0}%)";
    }
    
    public sealed class ArmorDamageModifierProcessor : IDefenseModifierProcessor
    {
        private readonly CombatEntityRegistry _combatEntityRegistry;

        public ArmorDamageModifierProcessor(CombatEntityRegistry combatEntityRegistry)
        {
            _combatEntityRegistry = combatEntityRegistry;
        }

        public Type ConfigType => typeof(ArmorDamageModifierConfig);

        public void Apply(DefenseModifierConfig config, DamageContext context)
        {
            if (context.Request.Payload.Type != DamageType.Physical) return;
            if (!_combatEntityRegistry.TryGet(context.Request.Target, out var entityState)) return;

            var armor = entityState.Stats.Get(StatType.Armor);
            if (armor <= 0f) return;

            var armorConfig = (ArmorDamageModifierConfig)config;
            var effectiveArmor = armor; //Mathf.Clamp01(1f - context.Request.Penetration.ArmorIgnorePercent);
            var reduction = Mathf.Clamp(effectiveArmor * armorConfig.ReductionPerArmor, 0f, armorConfig.MaxReduction);
            context.CurrentDamage *= 1f - reduction;
        }
    }
}
