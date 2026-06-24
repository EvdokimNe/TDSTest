using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    [CreateAssetMenu(menuName = "TDS/Arena/Schedule/Immediate")]
    public sealed class ImmediateSpawnConfig : SpawnScheduleConfig
    {
        public override SpawnScheduleState CreateState() => new ImmediateSpawnState();
    }

    public sealed class ImmediateSpawnState : SpawnScheduleState
    {
        public bool Executed;
    }

    public sealed class ImmediateSpawnProcessor : ISpawnScheduleProcessor
    {
        public Type ConfigType => typeof(ImmediateSpawnConfig);

        public int Tick(SpawnGroupContext ctx)
        {
            var state = (ImmediateSpawnState)ctx.State;
            if (state.Executed) return 0;

            state.Executed = true;
            var toSpawn = ctx.Group.Count - state.Spawned;
            state.Spawned = ctx.Group.Count;
            return toSpawn;
        }
    }
}
