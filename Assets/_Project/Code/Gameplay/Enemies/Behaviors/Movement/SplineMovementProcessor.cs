using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    public sealed class SplineMovementProcessor : IEnemyMovementProcessor
    {
        public Type ConfigType => typeof(SplineMovementBehaviorConfig);

        public void Tick(EnemyMovementContext ctx)
        {
            var config = (SplineMovementBehaviorConfig)ctx.Config;
            if (ctx.MovementState is not SplineMovementState state) return;

            state.Distance += config.Speed * ctx.DeltaTime;

            if (!state.Path.TryEvaluate(state.Distance, out var sample))
                return;

            if (sample.IsComplete)
            {
                if (config.Loop)
                    state.Distance = 0f;
                else
                    state.Distance = sample.Distance;
            }

            var body = ctx.Agent.View.transform;
            body.position = new Vector3(sample.Position.x, body.position.y, sample.Position.z);

            var forward = new Vector3(sample.Forward.x, 0f, sample.Forward.z);
            if (forward.sqrMagnitude > 0.001f)
                body.rotation = Quaternion.LookRotation(forward.normalized, Vector3.up);
        }
    }
}
