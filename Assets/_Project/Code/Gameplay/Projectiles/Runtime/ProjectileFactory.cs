using _Project.Code.Gameplay.Combat;
using _Project.Code.Shared;
using UnityEngine;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileFactory
    {
        private readonly InternalIntIdProvider _idProvider;
        private readonly ProjectilePoolProvider _poolProvider;
        private readonly ProjectileModificationPipeline _modificationPipeline;
        private readonly ProjectileRegistry _registry;

        public ProjectileFactory(
            InternalIntIdProvider idProvider,
            ProjectilePoolProvider poolProvider,
            ProjectileModificationPipeline modificationPipeline,
            ProjectileRegistry registry)
        {
            _idProvider = idProvider;
            _poolProvider = poolProvider;
            _modificationPipeline = modificationPipeline;
            _registry = registry;
        }

        public ProjectileAgent Create(ProjectileConfig config, Vector3 position, Vector3 direction, DamagePayload payload)
        {
            var id = _idProvider.Create();
            direction = direction.normalized;

            var pool = _poolProvider.GetOrCreate(config.Prefab, config.name);
            var view = pool.Rent();
            view.transform.position = position;
            view.transform.forward = direction;
            view.transform.localScale = config.Prefab.transform.localScale;

            var runtimeData = new ProjectileRuntimeData(direction, payload, config.Lifetime, view.Radius);
            var agent = new ProjectileAgent(id, view, config, runtimeData, pool);

            _modificationPipeline.Apply(agent, config.Modifiers);

            _registry.Add(agent);

            return agent;
        }
    }
}
