using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Shared;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyFactory
    {
        private readonly InternalIntIdProvider _idProvider;
        private readonly EnemyPoolProvider _poolProvider;
        private readonly EnemyRegistry _registry;
        private readonly CombatEntityRegistry _combatEntityRegistry;
        private readonly HittableRegistry _hittableRegistry;

        public EnemyFactory(
            InternalIntIdProvider idProvider,
            EnemyPoolProvider poolProvider,
            EnemyRegistry registry,
            CombatEntityRegistry combatEntityRegistry,
            HittableRegistry hittableRegistry)
        {
            _idProvider = idProvider;
            _poolProvider = poolProvider;
            _registry = registry;
            _combatEntityRegistry = combatEntityRegistry;
            _hittableRegistry = hittableRegistry;
        }

        public EnemyAgent Create(EnemyConfig config, Vector3 position)
        {
            var id = _idProvider.Create();

            var pool = _poolProvider.GetOrCreate(config.Prefab, config.name);
            var view = pool.Rent();
            view.transform.position = position;

            var runtimeData = new EnemyRuntimeData(config);

            if (config.MovementBehavior != null)
                runtimeData.MovementState = config.MovementBehavior.CreateState(view);

            if (config.Weapon != null)
                runtimeData.WeaponContext = config.Weapon.CreateContext();

            var agent = new EnemyAgent(id, view, runtimeData, pool);
            agent.Hittable = new HittableModule(agent, view.transform, view.Radius);

            _registry.Add(agent);

            config.StatsConfig.TryGetRequired(StatType.MaxHealth, out var maxHp);

            _combatEntityRegistry.Register(agent, config.StatsConfig, maxHp, config.DefenseModifiers);
            _hittableRegistry.Add(agent.Hittable);

            return agent;
        }
    }
}
