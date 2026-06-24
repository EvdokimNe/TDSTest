using System;
using _Project.Code.Gameplay.Enemies;

namespace _Project.Code.Gameplay.Arena
{
    [Serializable]
    public sealed class SpawnGroup
    {
        public EnemyConfig Enemy;
        public int Count = 1;
        public SpawnPlacementAuthoring Placement;
        public SpawnScheduleConfig Schedule;
    }
}
