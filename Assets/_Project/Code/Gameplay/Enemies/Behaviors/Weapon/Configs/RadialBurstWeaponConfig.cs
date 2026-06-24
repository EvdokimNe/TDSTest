using System;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Projectiles;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Weapon/RadialBurst")]
    public sealed class RadialBurstWeaponConfig : EnemyWeaponConfig
    {
        public ProjectileConfig ProjectileConfig;
        public int ProjectilesCount = 3;
        public float SpreadAngleDegrees = 20f;
    }
    
    public sealed class RadialBurstWeaponProcessor : IEnemyWeaponProcessor
    {
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly ProjectileFactory _projectileFactory;

        public Type ConfigType => typeof(RadialBurstWeaponConfig);

        public RadialBurstWeaponProcessor(DamagePayloadFactory payloadFactory, ProjectileFactory projectileFactory)
        {
            _payloadFactory = payloadFactory;
            _projectileFactory = projectileFactory;
        }

        public void Tick(EnemyWeaponContext ctx)
        {
            ctx.WeaponContext.CooldownRemaining -= ctx.DeltaTime;
            if (ctx.WeaponContext.CooldownRemaining > 0f) return;

            var config = (RadialBurstWeaponConfig)ctx.Config;
            ctx.WeaponContext.CooldownRemaining = config.Cooldown;

            var position = ctx.Agent.View.transform.position;
            var baseDirection = ctx.PlayerPosition - position;
            baseDirection.y = 0f;
            if (baseDirection.sqrMagnitude < 0.001f) return;
            baseDirection.Normalize();

            var payload = _payloadFactory.Resolve(ctx.Agent, config.AttackConfig);

            var count = config.ProjectilesCount;
            if (count == 1)
            {
                _projectileFactory.Create(config.ProjectileConfig, position, baseDirection, payload);
                return;
            }

            var halfSpread = config.SpreadAngleDegrees * 0.5f;
            var step = config.SpreadAngleDegrees / (count - 1);

            for (var i = 0; i < count; i++)
            {
                var angle = -halfSpread + step * i;
                var direction = Quaternion.AngleAxis(angle, Vector3.up) * baseDirection;
                _projectileFactory.Create(config.ProjectileConfig, position, direction, payload);
            }
        }
    }
}
