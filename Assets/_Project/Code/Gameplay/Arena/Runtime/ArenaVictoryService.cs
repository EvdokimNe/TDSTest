using _Project.Code.Gameplay.Enemies;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class ArenaVictoryService
    {
        private readonly ArenaController _controller;
        private readonly EnemyRegistry _enemyRegistry;
        private readonly ArenaResultStream _resultStream;

        private bool _published;

        public ArenaVictoryService(
            ArenaController controller,
            EnemyRegistry enemyRegistry,
            ArenaResultStream resultStream)
        {
            _controller = controller;
            _enemyRegistry = enemyRegistry;
            _resultStream = resultStream;
        }

        public void Tick()
        {
            if (_published || !_controller.IsActive) return;
            if (!_controller.IsSpawnFinished) return;
            if (_enemyRegistry.Count > 0) return;

            _published = true;
            _resultStream.Publish(ArenaResult.Victory);
        }
    }
}
