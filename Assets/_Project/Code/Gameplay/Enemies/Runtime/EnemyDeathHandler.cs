using System;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Despawn;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyDeathHandler : IStartable, IDisposable
    {
        private readonly CombatDeathStream _deathStream;
        private readonly DespawnSystem _despawnSystem;

        public EnemyDeathHandler(CombatDeathStream deathStream, DespawnSystem despawnSystem)
        {
            _deathStream = deathStream;
            _despawnSystem = despawnSystem;
        }

        public void Start() => _deathStream.Died += OnDied;

        public void Dispose() => _deathStream.Died -= OnDied;

        private void OnDied(ICombatEntity entity) => _despawnSystem.RequestEnemy(entity.Id);
    }
}
