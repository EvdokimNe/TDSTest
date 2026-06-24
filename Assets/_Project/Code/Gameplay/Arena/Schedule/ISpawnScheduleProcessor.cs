using System;

namespace _Project.Code.Gameplay.Arena
{
    public interface ISpawnScheduleProcessor
    {
        Type ConfigType { get; }
        int Tick(SpawnGroupContext ctx);
    }
}
