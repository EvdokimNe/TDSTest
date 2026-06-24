using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Enemies.Behaviors.Movement;
using _Project.Code.Gameplay.Player;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyMovementService
    {
        private readonly EnemyRegistry _registry;
        private readonly PlayerProvider _playerProvider;
        private readonly Dictionary<Type, IEnemyMovementProcessor> _processors = new();
        private readonly EnemyMovementContext _context = new();

        public EnemyMovementService(
            EnemyRegistry registry,
            PlayerProvider playerProvider,
            IEnumerable<IEnemyMovementProcessor> processors)
        {
            _registry = registry;
            _playerProvider = playerProvider;

            foreach (var processor in processors)
                _processors[processor.ConfigType] = processor;
        }

        public void Tick(float deltaTime)
        {
            var playerPosition = _playerProvider.PlayerView != null
                ? _playerProvider.PlayerView.transform.position
                : Vector3.zero;

            var items = _registry.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var agent = items[i];
                if (!agent.RuntimeData.IsAlive) continue;

                var config = agent.RuntimeData.Config.MovementBehavior;
                if (config == null) continue;
                if (!_processors.TryGetValue(config.GetType(), out var processor)) continue;

                _context.Agent = agent;
                _context.Config = config;
                _context.MovementState = agent.RuntimeData.MovementState;
                _context.PlayerPosition = playerPosition;
                _context.DeltaTime = deltaTime;

                processor.Tick(_context);
            }
        }
    }
}
