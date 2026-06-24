using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    public sealed class ChasePlayerMovementProcessor : IEnemyMovementProcessor
    {
        public Type ConfigType => typeof(ChasePlayerMovementBehaviorConfig);

        public void Tick(EnemyMovementContext ctx)
        {
            var config = (ChasePlayerMovementBehaviorConfig)ctx.Config;
            var body = ctx.Agent.View.transform;
            var current = body.position;
            var target = new Vector3(ctx.PlayerPosition.x, current.y, ctx.PlayerPosition.z);

            var dx = current.x - target.x;
            var dz = current.z - target.z;
            if (dx * dx + dz * dz <= config.StopDistance * config.StopDistance)
                return;

            var next = Vector3.MoveTowards(current, target, config.Speed * ctx.DeltaTime);
            var delta = next - current;
            body.position = next;

            if (delta.sqrMagnitude > 0.0001f)
                body.rotation = Quaternion.LookRotation(delta.normalized, Vector3.up);
        }
    }
}
