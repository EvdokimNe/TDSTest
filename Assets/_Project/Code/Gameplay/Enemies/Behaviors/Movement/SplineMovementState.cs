using _Project.Code.Gameplay.Paths.Contracts;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    public sealed class SplineMovementState : EnemyMovementState
    {
        public readonly ISplinePath Path;
        public float Distance;

        public SplineMovementState(ISplinePath path)
        {
            Path = path;
        }
    }
}
