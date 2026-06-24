using System.Collections.Generic;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class CombatEntityRegistry
    {
        private readonly Dictionary<InternalIntId, CombatEntityState> _statesByEntityId = new();

        public CombatEntityState Register(ICombatEntity entity, StatsConfig statsConfig, float maxHealth, IReadOnlyList<DefenseModifierConfig> defenseModifiers)
        {
            var stats = new StatsContainer();
            stats.Apply(statsConfig);

            _statesByEntityId[entity.Id] = new CombatEntityState(
                new HealthState(maxHealth),
                stats,
                defenseModifiers);

            return _statesByEntityId[entity.Id];
        }

        public void Unregister(ICombatEntity entity)
        {
            _statesByEntityId.Remove(entity.Id);
        }

        public bool TryGet(ICombatEntity entity, out CombatEntityState state)
        {
            return _statesByEntityId.TryGetValue(entity.Id, out state);
        }
    }
}
