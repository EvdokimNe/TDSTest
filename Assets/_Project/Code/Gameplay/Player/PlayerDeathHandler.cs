using System;
using _Project.Code.Gameplay.Arena;
using _Project.Code.Gameplay.Combat;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Player
{
    public sealed class PlayerDeathHandler : IStartable, IDisposable
    {
        private readonly CombatDeathStream _deathStream;
        private readonly ArenaResultStream _resultStream;

        public PlayerDeathHandler(CombatDeathStream deathStream, ArenaResultStream resultStream)
        {
            _deathStream = deathStream;
            _resultStream = resultStream;
        }

        public void Start() => _deathStream.Died += OnDied;

        public void Dispose() => _deathStream.Died -= OnDied;

        private void OnDied(ICombatEntity entity)
        {
            if (entity.Team == CombatTeam.Player)
                _resultStream.Publish(ArenaResult.Defeat);
        }
    }
}
