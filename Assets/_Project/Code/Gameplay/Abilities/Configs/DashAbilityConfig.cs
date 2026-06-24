using System;
using _Project.Code.Gameplay.Player;
using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "TDS/Abilities/Dash")]
    public sealed class DashAbilityConfig : AbilityConfig
    {
        public float Distance = 5f;
        public float Duration = 0.15f;

        public override bool RequiresTargeting => false;
    }

    public sealed class DashAbilityProcessor : IAbilityProcessor
    {
        private readonly PlayerProvider _player;

        public Type ConfigType => typeof(DashAbilityConfig);

        public DashAbilityProcessor(PlayerProvider player)
        {
            _player = player;
        }

        public void Execute(AbilityConfig config, AbilityContext ctx)
        {
            var cfg = (DashAbilityConfig)config;
            _player.Motor.TryStartForcedMove(ctx.AimDirection, cfg.Distance, cfg.Duration);
        }
    }
}
