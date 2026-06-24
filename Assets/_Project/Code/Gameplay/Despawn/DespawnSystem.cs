using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Enemies;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Gameplay.Projectiles;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Despawn
{
   public sealed class DespawnSystem
    {
        private enum Kind
        {
            Enemy,
            Projectile
        }

        private readonly EnemyRegistry _enemies;
        private readonly ProjectileRegistry _projectiles;
        private readonly CombatEntityRegistry _combat;
        private readonly HittableRegistry _hittable;
        private readonly EnemyDiedEventStream _enemyDied;

        private readonly List<(Kind kind, InternalIntId id)> _pending = new(128);

        public DespawnSystem(
            EnemyRegistry enemies,
            ProjectileRegistry projectiles,
            CombatEntityRegistry combat,
            HittableRegistry hittable,
            EnemyDiedEventStream enemyDied)
        {
            _enemies = enemies;
            _projectiles = projectiles;
            _combat = combat;
            _hittable = hittable;
            _enemyDied = enemyDied;
        }

        public void RequestEnemy(InternalIntId id)
        {
            if (_enemies.TryGet(id, out var agent) && agent.RuntimeData.IsAlive)
            {
                agent.RuntimeData.IsAlive = false;
                _pending.Add((Kind.Enemy, id));
            }
        }

        public void RequestProjectile(InternalIntId id)
        {
            if (_projectiles.TryGet(id, out var agent) && agent.RuntimeData.IsAlive)
            {
                agent.RuntimeData.IsAlive = false;
                _pending.Add((Kind.Projectile, id));
            }
        }

        public void Tick()
        {
            for (var i = 0; i < _pending.Count; i++)
            {
                var (kind, id) = _pending[i];
                switch (kind)
                {
                    case Kind.Enemy:
                        TeardownEnemy(id);
                        break;
                    case Kind.Projectile:
                        TeardownProjectile(id);
                        break;
                }
            }

            _pending.Clear();
        }

        private void TeardownEnemy(InternalIntId id)
        {
            if (!_enemies.TryGet(id, out var agent))
                return;

            agent.Pool.Return(agent.View);
            _enemies.Remove(id);
            _combat.Unregister(agent);
            _hittable.Remove(id);
            _enemyDied.Publish(id);
        }

        private void TeardownProjectile(InternalIntId id)
        {
            if (!_projectiles.TryGet(id, out var agent))
                return;

            agent.Pool.Return(agent.View);
            _projectiles.Remove(id);
        }
    }
}
