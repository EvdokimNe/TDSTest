using System;

namespace _Project.Code.Gameplay.Arena
{
    public enum ArenaResult { Victory, Defeat }

    public sealed class ArenaResultStream
    {
        public event Action<ArenaResult> Finished;

        public void Publish(ArenaResult result) => Finished?.Invoke(result);
    }
}
