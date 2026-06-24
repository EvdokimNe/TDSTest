using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "TDS/Abilities/Aoe")]
    public sealed class AoeAbilityConfig : AbilityConfig
    {
        public float Radius = 3f;
        public AttackConfig AttackConfig;

        public override bool RequiresTargeting => true;
    }

    public sealed class AoeAbilityProcessor : IAbilityProcessor
    {
        private readonly HitQueryService _hitQuery;
        private readonly DamagePayloadFactory _payloadFactory;
        private readonly DamageApplicationService _damageApplication;
        private readonly List<HittableModule> _buffer = new(32);

        public Type ConfigType => typeof(AoeAbilityConfig);

        public AoeAbilityProcessor(
            HitQueryService hitQuery,
            DamagePayloadFactory payloadFactory,
            DamageApplicationService damageApplication)
        {
            _hitQuery = hitQuery;
            _payloadFactory = payloadFactory;
            _damageApplication = damageApplication;
        }

        public void Execute(AbilityConfig config, AbilityContext ctx)
        {
            var cfg = (AoeAbilityConfig)config;

            _hitQuery.OverlapCircle(ctx.AimPoint, cfg.Radius, CombatTeam.Player, _buffer);
            if (_buffer.Count == 0) return;

            var payload = _payloadFactory.Resolve(ctx.Source, cfg.AttackConfig);

            for (var i = 0; i < _buffer.Count; i++)
                _damageApplication.ApplyDamage(payload.CreateRequest(_buffer[i].Entity));
        }
    }
}
