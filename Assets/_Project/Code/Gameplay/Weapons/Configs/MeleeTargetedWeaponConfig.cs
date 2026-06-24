using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "TDS/Weapons/Melee Targeted")]
    public sealed class MeleeTargetedWeaponConfig : WeaponConfig
    {
        public float Range = 2f;
        public float PickRadius = 0.2f;
    }

    public sealed class MeleeTargetedWeaponProcessor : IWeaponProcessor
    {
        private readonly HitQueryService _hitQuery;
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly DamageApplicationService _damageApplication;
        private readonly List<HittableModule> _buffer = new(16);

        public Type ConfigType => typeof(MeleeTargetedWeaponConfig);

        public MeleeTargetedWeaponProcessor(
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
            var cfg = (MeleeTargetedWeaponConfig)config;

            _hitQuery.OverlapCircle(ctx.AimPoint, cfg.PickRadius, CombatTeam.Player, _buffer);
            if (_buffer.Count == 0) return;

            var target = PickNearestToCursor(ctx.AimPoint);
            if (target == null) return;

            var dx = target.Position.x - ctx.Origin.x;
            var dz = target.Position.z - ctx.Origin.z;
            if (dx * dx + dz * dz > cfg.Range * cfg.Range) return;

            var payload = _payloadFactory.Resolve(ctx.Source, cfg.AttackConfig);
            _damageApplication.ApplyDamage(payload.CreateRequest(target.Entity));
        }

        private HittableModule PickNearestToCursor(Vector3 cursor)
        {
            HittableModule nearest = null;
            var bestSqr = float.MaxValue;

            for (var i = 0; i < _buffer.Count; i++)
            {
                var hittable = _buffer[i];
                var dx = hittable.Position.x - cursor.x;
                var dz = hittable.Position.z - cursor.z;
                var sqr = dx * dx + dz * dz;
                if (sqr < bestSqr)
                {
                    bestSqr = sqr;
                    nearest = hittable;
                }
            }

            return nearest;
        }
    }
}
