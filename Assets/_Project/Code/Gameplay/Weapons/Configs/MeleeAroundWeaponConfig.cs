using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "TDS/Weapons/Melee Around")]
    public sealed class MeleeAroundWeaponConfig : WeaponConfig
    {
        public float Radius = 2.5f;
    }

    public sealed class MeleeAroundWeaponProcessor : IWeaponProcessor
    {
        private readonly HitQueryService _hitQuery;
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly DamageApplicationService _damageApplication;
        private readonly List<HittableModule> _buffer = new(32);

        public Type ConfigType => typeof(MeleeAroundWeaponConfig);

        public MeleeAroundWeaponProcessor(
            HitQueryService hitQuery,
            DamagePayloadFactory payloadFactory,
            DamageApplicationService damageApplication)
        {
            _hitQuery = hitQuery;
            _payloadFactory = payloadFactory;
            _damageApplication = damageApplication;
        }

        public void Fire(WeaponConfig config, WeaponContext ctx)
        {
            var cfg = (MeleeAroundWeaponConfig)config;

            _hitQuery.OverlapCircle(ctx.Origin, cfg.Radius, CombatTeam.Player, _buffer);
            if (_buffer.Count == 0) return;

            var payload = _payloadFactory.Resolve(ctx.Source, cfg.AttackConfig);

            for (var i = 0; i < _buffer.Count; i++)
                _damageApplication.ApplyDamage(payload.CreateRequest(_buffer[i].Entity));
        }
    }
}
