using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class ArenaProvider : MonoBehaviour
    {
        [SerializeField] private ArenaTriggerView _trigger;
        [SerializeField] private List<SpawnGroup> _spawnGroups = new();

        public ArenaTriggerView Trigger => _trigger;
        public IReadOnlyList<SpawnGroup> SpawnGroups => _spawnGroups;
    }
}
