using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public abstract class SpawnScheduleConfig : ScriptableObject
    {
        public abstract SpawnScheduleState CreateState();
    }
}
