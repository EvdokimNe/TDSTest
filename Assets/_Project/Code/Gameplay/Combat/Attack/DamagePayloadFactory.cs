using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class DamagePayloadFactory
    {
        private readonly Dictionary<Type, IPreResolvedModifierProcessor> _preResolvedProcessorsByType = new();
        
        private readonly DamageResolutionContext _context = new();

        public DamagePayloadFactory(
            IEnumerable<IPreResolvedModifierProcessor> preResolvedProcessors)
        {
            foreach (var processor in preResolvedProcessors)
                _preResolvedProcessorsByType[processor.ConfigType] = processor;
        }

        public DamagePayload Resolve(ICombatEntity source, AttackConfig attackConfig)
        {
            if (source == null || attackConfig == null)
                return default;

            _context.Reset(source, attackConfig);
            ApplyPreResolvedModifiers(attackConfig.PreResolvedModifiers);

            return new DamagePayload(
                source,
                _context.Damage,
                _context.DamageType,
                _context.DamageTags,
                _context.OnHitModifiers);
        }

        private void ApplyPreResolvedModifiers(IReadOnlyList<PreResolvedModifierConfig> modifiers)
        {
            if (modifiers == null)
                return;

            for (var i = 0; i < modifiers.Count; i++)
            {
                var modifier = modifiers[i];
                if (modifier == null)
                    continue;

                if (_preResolvedProcessorsByType.TryGetValue(modifier.GetType(), out var processor))
                {
                    processor.Apply(modifier, _context);
                    continue;
                }

                Debug.LogError($"[AttackResolverService] Missing pre-resolved processor for {modifier.GetType().Name}.");
            }
        }
    }
}
