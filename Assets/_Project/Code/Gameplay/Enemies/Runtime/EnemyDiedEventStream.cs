using System;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyDiedEventStream
    {
        public event Action<InternalIntId> EnemyDied;

        public void Publish(InternalIntId id) => EnemyDied?.Invoke(id);
    }
}
