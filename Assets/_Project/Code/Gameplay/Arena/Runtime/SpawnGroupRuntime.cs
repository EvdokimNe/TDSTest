namespace _Project.Code.Gameplay.Arena
{
    public sealed class SpawnGroupRuntime
    {
        public readonly SpawnGroup Group;
        public readonly SpawnScheduleState State;

        public bool IsFinished => State.IsFinished(Group.Count);

        public SpawnGroupRuntime(SpawnGroup group)
        {
            Group = group;
            State = group.Schedule.CreateState();
        }
    }
}
