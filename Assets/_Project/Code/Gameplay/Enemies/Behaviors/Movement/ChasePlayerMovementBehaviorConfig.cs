using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Movement/ChasePlayer")]
    public sealed class ChasePlayerMovementBehaviorConfig : EnemyMovementBehaviorConfig
    {
        public float Speed = 4f;
        public float StopDistance = 1f;
    }
}
