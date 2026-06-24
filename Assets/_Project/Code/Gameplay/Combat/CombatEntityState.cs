using System.Collections.Generic;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class CombatEntityState
    {
        public HealthState Health;
        public StatsContainer Stats;
        public IReadOnlyList<DefenseModifierConfig> DefenseModifiers;

        public CombatEntityState(HealthState health, StatsContainer stats, IReadOnlyList<DefenseModifierConfig> defenseModifiers)
        {
            Health = health;
            Stats = stats;
            DefenseModifiers = defenseModifiers;
        }
    }
}
