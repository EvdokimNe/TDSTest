using System;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Projectiles;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "TDS/Weapons/Ranged")]
    public sealed class RangedWeaponConfig : WeaponConfig
    {
        public ProjectileConfig ProjectileConfig;
    }

    public sealed class RangedWeaponProcessor : IWeaponProcessor
    {
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly ProjectileFactory _projectileFactory;

        public Type ConfigType => typeof(RangedWeaponConfig);

        public RangedWeaponProcessor(DamagePayloadFactory payloadFactory, ProjectileFactory projectileFactory)
        {
            _payloadFactory = payloadFactory;
            _projectileFactory = projectileFactory;
        }

        public void Fire(WeaponConfig config, WeaponContext ctx)
        {
            var cfg = (RangedWeaponConfig)config;

            var direction = ctx.AimDirection;
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.0001f) return;

            var payload = _payloadFactory.Resolve(ctx.Source, cfg.AttackConfig);
            _projectileFactory.Create(cfg.ProjectileConfig, ctx.MuzzlePosition, direction, payload);
        }
    }
}
