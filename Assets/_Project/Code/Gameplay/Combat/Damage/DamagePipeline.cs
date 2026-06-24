using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class DamagePipeline
    {
        private readonly CombatEntityRegistry _registry;
        private readonly Dictionary<Type, IOnHitModifierProcessor> _onHitProcessorsByType = new();
        private readonly Dictionary<Type, IDefenseModifierProcessor> _defenseProcessorsByType = new();
        
        private readonly DamageContext _context = new();

        public DamagePipeline(
            IEnumerable<IOnHitModifierProcessor> onHitProcessors,
            IEnumerable<IDefenseModifierProcessor> defenseProcessors)
        {
            foreach (var processor in onHitProcessors)
                _onHitProcessorsByType[processor.ConfigType] = processor;

            foreach (var processor in defenseProcessors)
                _defenseProcessorsByType[processor.ConfigType] = processor;
        }

        public DamageCalculation Calculate(DamageRequest request, CombatEntityState targetState)
        {
            _context.Reset(request);
            ApplyOnHitModifiers(request.Payload.OnHitModifiers);
            ApplyDefenseModifiers(targetState.DefenseModifiers);
            
            return new DamageCalculation(_context.CurrentDamage);
        }
       

        private void ApplyOnHitModifiers(IReadOnlyList<OnHitModifierConfig> modifiers)
        {
            if (modifiers == null)
                return;

            for (var i = 0; i < modifiers.Count; i++)
            {
                var modifier = modifiers[i];
                if (modifier == null)
                    continue;

                if (_onHitProcessorsByType.TryGetValue(modifier.GetType(), out var processor))
                {
                    processor.Apply(modifier, _context);
                    continue;
                }

                Debug.LogError($"[DamagePipeline] Missing on-hit processor for modifier {modifier.GetType().Name}.");
            }
        }

        private void ApplyDefenseModifiers(IReadOnlyList<DefenseModifierConfig> modifiers)
        {
            if (modifiers == null)
                return;

            for (var i = 0; i < modifiers.Count; i++)
            {
                var modifier = modifiers[i];
                if (modifier == null)
                    continue;

                if (_defenseProcessorsByType.TryGetValue(modifier.GetType(), out var processor))
                {
                    processor.Apply(modifier, _context);
                    continue;
                }

                Debug.LogError($"[DamagePipeline] Missing defense processor for modifier {modifier.GetType().Name}.");
            }
        }
    }
}
