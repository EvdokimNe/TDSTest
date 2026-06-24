using System.Collections.Generic;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class StatsContainer
    {
        private readonly Dictionary<StatType, float> _values = new();
        
        public float Get(StatType type)
        {
            return _values.TryGetValue(type, out var value) ? value : 0f;
        }

        public void Set(StatType type, float value)
        {
            _values[type] = value;
        }

        public void Apply(StatsConfig config)
        {
            if (config == null || config.Values == null)
                return;

            for (var i = 0; i < config.Values.Count; i++)
            {
                _values[config.Values[i].type] = config.Values[i].value;
            }
        }
    }
}
