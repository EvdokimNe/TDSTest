using System.Collections.Generic;
using UnityEngine;
using uPools;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectilePoolProvider
    {
        private readonly Dictionary<string, ObjectPool<ProjectileView>> _pools = new();

        public ObjectPool<ProjectileView> GetOrCreate(ProjectileView prefab, string poolId)
        {
            if (_pools.TryGetValue(poolId, out var pool))
                return pool;

            pool = new ObjectPool<ProjectileView>(
                createFunc: () => Object.Instantiate(prefab),
                onRent: view => view.gameObject.SetActive(true),
                onReturn: view => view.gameObject.SetActive(false),
                onDestroy: view => Object.Destroy(view.gameObject)
            );

            _pools[poolId] = pool;
            return pool;
        }
    }
}
