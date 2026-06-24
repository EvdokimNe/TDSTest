using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Enemies.Behaviors.Weapon;
using _Project.Code.Gameplay.Player;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyWeaponService
    {
        private readonly EnemyRegistry _registry;
        private readonly PlayerProvider _playerProvider;
        private readonly Dictionary<Type, IEnemyWeaponProcessor> _processors = new();
        private readonly EnemyWeaponContext _context = new();

        public EnemyWeaponService(
            EnemyRegistry registry,
            PlayerProvider playerProvider,
            IEnumerable<IEnemyWeaponProcessor> processors)
        {
            _registry = registry;
            _playerProvider = playerProvider;

            foreach (var processor in processors)
                _processors[processor.ConfigType] = processor;
        }

        public void Tick(float deltaTime)
        {
            var playerView = _playerProvider.PlayerView;
            var playerPosition = playerView != null ? playerView.transform.position : Vector3.zero;
            ICombatEntity playerTarget = _playerProvider.Combat;

            var items = _registry.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var agent = items[i];
                if (!agent.RuntimeData.IsAlive) continue;

                var config = agent.RuntimeData.Config.Weapon;
                if (config == null) continue;
                if (!_processors.TryGetValue(config.GetType(), out var processor)) continue;

                _context.Agent = agent;
                _context.Config = config;
                _context.WeaponContext = agent.RuntimeData.WeaponContext;
                _context.PlayerPosition = playerPosition;
                _context.PlayerTarget = playerTarget;
                _context.DeltaTime = deltaTime;

                processor.Tick(_context);
            }
        }
    }
}
