using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileModificationPipeline
    {
        private readonly Dictionary<Type, IProjectileModifierProcessor> _processorsByType = new();
        private readonly ProjectileModificationContext _context = new();

        public ProjectileModificationPipeline(IEnumerable<IProjectileModifierProcessor> processors)
        {
            foreach (var processor in processors)
                _processorsByType[processor.ConfigType] = processor;
        }

        public void Apply(ProjectileAgent agent, IReadOnlyList<ProjectileModifierConfig> modifiers)
        {
            if (modifiers == null || modifiers.Count == 0)
                return;

            _context.Reset(agent);

            for (var i = 0; i < modifiers.Count; i++)
            {
                var modifier = modifiers[i];
                if (modifier == null)
                    continue;

                if (_processorsByType.TryGetValue(modifier.GetType(), out var processor))
                {
                    processor.Apply(modifier, _context);
                    continue;
                }

                Debug.LogError($"[ProjectileModificationPipeline] Missing processor for {modifier.GetType().Name}.");
            }

            agent.RuntimeData.SpeedMultiplier = _context.SpeedMultiplier;
            agent.RuntimeData.Radius *= _context.RadiusMultiplier;
            agent.View.transform.localScale *= _context.ScaleMultiplier;
        }
    }
}
