using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Enemies;
using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class ArenaController
    {
        private readonly ArenaProvider _provider;
        private readonly EnemyFactory _enemyFactory;
        private readonly Dictionary<Type, ISpawnScheduleProcessor> _processors = new();
        private readonly SpawnGroupContext _context = new();

        private List<SpawnGroupRuntime> _groups;
        private bool _active;

        public bool IsActive => _active;

        public bool IsSpawnFinished
        {
            get
            {
                if (!_active || _groups == null) return false;
                for (var i = 0; i < _groups.Count; i++)
                    if (!_groups[i].IsFinished) return false;
                return true;
            }
        }

        public ArenaController(
            ArenaProvider provider,
            EnemyFactory enemyFactory,
            IEnumerable<ISpawnScheduleProcessor> processors)
        {
            _provider = provider;
            _enemyFactory = enemyFactory;

            foreach (var p in processors)
                _processors[p.ConfigType] = p;

            _provider.Trigger.PlayerEntered += Activate;
        }

        public void Tick(float deltaTime)
        {
            if (!_active) return;

            for (var i = 0; i < _groups.Count; i++)
            {
                var runtime = _groups[i];
                if (runtime.IsFinished) continue;

                var config = runtime.Group.Schedule;
                if (!_processors.TryGetValue(config.GetType(), out var processor)) continue;

                _context.Group = runtime.Group;
                _context.Config = config;
                _context.State = runtime.State;
                _context.DeltaTime = deltaTime;

                var count = processor.Tick(_context);
                for (var j = 0; j < count; j++)
                {
                    var position = runtime.Group.Placement != null
                        ? runtime.Group.Placement.NextPoint()
                        : Vector3.zero;

                    _enemyFactory.Create(runtime.Group.Enemy, position);
                }
            }
        }

        private void Activate()
        {
            _provider.Trigger.gameObject.SetActive(false);
            
            _groups = new List<SpawnGroupRuntime>(_provider.SpawnGroups.Count);
            foreach (var group in _provider.SpawnGroups)
                _groups.Add(new SpawnGroupRuntime(group));

            _active = true;
        }
    }
}
