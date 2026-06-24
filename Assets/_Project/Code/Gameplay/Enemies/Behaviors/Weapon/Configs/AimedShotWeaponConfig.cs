using System;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Projectiles;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Weapon/AimedShot")]
    public sealed class AimedShotWeaponConfig : EnemyWeaponConfig
    {
        public ProjectileConfig ProjectileConfig;
    }
    
    public sealed class AimedShotWeaponProcessor : IEnemyWeaponProcessor
    {
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly ProjectileFactory _projectileFactory;

        public Type ConfigType => typeof(AimedShotWeaponConfig);

        public AimedShotWeaponProcessor(DamagePayloadFactory payloadFactory, ProjectileFactory projectileFactory)
        {
            _payloadFactory = payloadFactory;
            _projectileFactory = projectileFactory;
        }

        public void Tick(EnemyWeaponContext ctx)
        {
            ctx.WeaponContext.CooldownRemaining -= ctx.DeltaTime;
            if (ctx.WeaponContext.CooldownRemaining > 0f) return;

            var config = (AimedShotWeaponConfig)ctx.Config;
            ctx.WeaponContext.CooldownRemaining = config.Cooldown;

            var position = ctx.Agent.View.transform.position;
            var direction = ctx.PlayerPosition - position;
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.001f) return;

            var payload = _payloadFactory.Resolve(ctx.Agent, config.AttackConfig);
            _projectileFactory.Create(config.ProjectileConfig, position, direction, payload);
        }
    }
}
