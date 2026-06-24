using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    [CreateAssetMenu(menuName = "TDS/Enemies/Movement/Spline")]
    public sealed class SplineMovementBehaviorConfig : EnemyMovementBehaviorConfig
    {
        public float Speed = 3f;
        public bool Loop = true;

        public override EnemyMovementState CreateState(EnemyView view)
        {
            //if (view.SplinePathProvider != null && view.SplinePathProvider.TryGetPath(out var path))
              //  return new SplineMovementState(path);

            return null;
        }
    }
}
