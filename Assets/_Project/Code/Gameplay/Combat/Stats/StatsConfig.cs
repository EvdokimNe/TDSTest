using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "TDS/Combat/Stats Config")]
    public sealed class StatsConfig : ScriptableObject
    {
        public List<StatEntry> Values = new();

        [Serializable]
        public struct StatEntry
        {
            public StatType type;
            public float value;
        }
        
        public bool TryGet(StatType type, out float value)
        {
            for (var i = 0; i < Values.Count; i++)
            {
                if (Values[i].type != type)
                    continue;

                value = Values[i].value;
                return true;
            }

            value = 0f;
            return false;
        }
        
        public bool TryGetRequired(StatType type, out float value)
        {
            if (TryGet(type, out value))
                return true;

            Debug.LogError($"[StatsConfig] '{name}' has no required stat '{type}'.", this);
            return false;
        }

        public float GetOrDefault(StatType type, float fallback = 0f)
        {
            return TryGet(type, out var value) ? value : fallback;
        }
    }
}
