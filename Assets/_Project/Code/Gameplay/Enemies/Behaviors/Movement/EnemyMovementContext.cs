using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    public sealed class EnemyMovementContext
    {
        public EnemyAgent Agent;
        public EnemyMovementBehaviorConfig Config;
        public EnemyMovementState MovementState;
        public Vector3 PlayerPosition;
        public float DeltaTime;
    }
}
