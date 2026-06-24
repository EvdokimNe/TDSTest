using System;
using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Projectiles
{
    [CreateAssetMenu(menuName = "TDS/Projectiles/Modifiers/Critical")]
    public sealed class CriticalProjectileModifierConfig : ProjectileModifierConfig
    {
        public float SpeedMultiplier = 1.5f;
        public float ScaleMultiplier = 1.5f;
        public float RadiusMultiplier = 1.5f;
    }

    public sealed class CriticalProjectileModifierProcessor : IProjectileModifierProcessor
    {
        public Type ConfigType => typeof(CriticalProjectileModifierConfig);

        public void Apply(ProjectileModifierConfig config, ProjectileModificationContext context)
        {
            if (!context.Payload.Tags.HasFlag(DamageTags.Critical))
                return;

            var cfg = (CriticalProjectileModifierConfig)config;
            context.SpeedMultiplier *= cfg.SpeedMultiplier;
            context.ScaleMultiplier *= cfg.ScaleMultiplier;
            context.RadiusMultiplier *= cfg.RadiusMultiplier;
        }
    }
}
