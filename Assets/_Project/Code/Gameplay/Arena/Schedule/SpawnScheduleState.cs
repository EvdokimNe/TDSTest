namespace _Project.Code.Gameplay.Arena
{
    public class SpawnScheduleState
    {
        public int Spawned;

        public bool IsFinished(int count) => Spawned >= count;
    }
}
