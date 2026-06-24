using System;
using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Weapon/Melee")]
    public sealed class MeleeWeaponConfig : EnemyWeaponConfig
    {
        public float Range = 1.5f;
    }
    
    public sealed class MeleeWeaponProcessor : IEnemyWeaponProcessor
    {
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly DamageApplicationService _damageApplicationService;

        public Type ConfigType => typeof(MeleeWeaponConfig);

        public MeleeWeaponProcessor(DamagePayloadFactory payloadFactory, DamageApplicationService damageApplicationService)
        {
            _payloadFactory = payloadFactory;
            _damageApplicationService = damageApplicationService;
        }

        public void Tick(EnemyWeaponContext ctx)
        {
            ctx.WeaponContext.CooldownRemaining -= ctx.DeltaTime;

            var config = (MeleeWeaponConfig)ctx.Config;
            var position = ctx.Agent.View.transform.position;
            var player = ctx.PlayerPosition;

            var dx = position.x - player.x;
            var dz = position.z - player.z;
            if (dx * dx + dz * dz > config.Range * config.Range) return;
            if (ctx.WeaponContext.CooldownRemaining > 0f) return;

            ctx.WeaponContext.CooldownRemaining = config.Cooldown;

            var payload = _payloadFactory.Resolve(ctx.Agent, config.AttackConfig);
            _damageApplicationService.ApplyDamage(new DamageRequest(payload, ctx.PlayerTarget));
        }
    }
}
