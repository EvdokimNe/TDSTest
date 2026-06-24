using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Code.Gameplay.Abilities
{
    [CreateAssetMenu(menuName = "TDS/Abilities/Loadout")]
    public sealed class AbilityLoadout : ScriptableObject
    {
        [Serializable]
        public struct Entry
        {
            public AbilityConfig Ability;
            public InputActionReference Input;
        }

        public List<Entry> Abilities = new();
    }
}
