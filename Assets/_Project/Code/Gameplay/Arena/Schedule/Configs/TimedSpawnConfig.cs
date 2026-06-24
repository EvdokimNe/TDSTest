using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    [CreateAssetMenu(menuName = "TDS/Arena/Schedule/Timed")]
    public sealed class TimedSpawnConfig : SpawnScheduleConfig
    {
        public float Interval = 2f;

        public override SpawnScheduleState CreateState() => new TimedSpawnState();
    }

    public sealed class TimedSpawnState : SpawnScheduleState
    {
        public float Timer;
    }

    public sealed class TimedSpawnProcessor : ISpawnScheduleProcessor
    {
        public Type ConfigType => typeof(TimedSpawnConfig);

        public int Tick(SpawnGroupContext ctx)
        {
            var config = (TimedSpawnConfig)ctx.Config;
            var state = (TimedSpawnState)ctx.State;

            if (state.Spawned >= ctx.Group.Count) return 0;

            state.Timer -= ctx.DeltaTime;
            if (state.Timer > 0f) return 0;

            state.Timer = config.Interval;
            state.Spawned++;
            return 1;
        }
    }
}
