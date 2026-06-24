using System;
using System.Linq;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Enemies.Providers;
using _Project.Code.Gameplay.Projectiles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Weapon/CircleShoot")]
    public sealed class CircleShootWeaponConfig : EnemyWeaponConfig
    {
        public ProjectileConfig ProjectileConfig;
        public float AngleStepDegrees = 10f;

        public override WeaponContext CreateContext() => new CircleShootWeaponContext();
    }

    public sealed class CircleShootWeaponContext : WeaponContext
    {
        public float CurrentAngleDegrees = Random.Range(0f, 360f);
    }

    public sealed class CircleShootWeaponProcessor : IEnemyWeaponProcessor
    {
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly ProjectileFactory _projectileFactory;

        public Type ConfigType => typeof(CircleShootWeaponConfig);

        public CircleShootWeaponProcessor(DamagePayloadFactory payloadFactory, ProjectileFactory projectileFactory)
        {
            _payloadFactory = payloadFactory;
            _projectileFactory = projectileFactory;
        }

        public void Tick(EnemyWeaponContext ctx)
        {
            ctx.WeaponContext.CooldownRemaining -= ctx.DeltaTime;
            if (ctx.WeaponContext.CooldownRemaining > 0f) return;

            var config = (CircleShootWeaponConfig)ctx.Config;
            var circleCtx = (CircleShootWeaponContext)ctx.WeaponContext;
            ctx.WeaponContext.CooldownRemaining = config.Cooldown;

            var position = ctx.Agent.View.transform.position;

            var shootPointProvider = (ShootPointProvider)ctx.Agent.View.ParamsProviders.FirstOrDefault(x => x.GetType() == typeof(ShootPointProvider));
            if (shootPointProvider != null)
            {
                position = shootPointProvider.Pos;
            }

            var angleRad = circleCtx.CurrentAngleDegrees * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Sin(angleRad), 0f, Mathf.Cos(angleRad));

            var payload = _payloadFactory.Resolve(ctx.Agent, config.AttackConfig);
            _projectileFactory.Create(config.ProjectileConfig, position, direction, payload);

            circleCtx.CurrentAngleDegrees = (circleCtx.CurrentAngleDegrees + config.AngleStepDegrees) % 360f;
        }
    }
}
