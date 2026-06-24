using System.Collections.Generic;
using UnityEngine;
using uPools;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyPoolProvider
    {
        private readonly Dictionary<string, ObjectPool<EnemyView>> _pools = new();

        public ObjectPool<EnemyView> GetOrCreate(EnemyView prefab, string poolId)
        {
            if (_pools.TryGetValue(poolId, out var pool))
                return pool;

            pool = new ObjectPool<EnemyView>(
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
