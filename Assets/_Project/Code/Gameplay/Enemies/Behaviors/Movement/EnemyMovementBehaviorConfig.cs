using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    public abstract class EnemyMovementBehaviorConfig : ScriptableObject
    {
        public virtual EnemyMovementState CreateState(EnemyView view) => null;
    }
}
