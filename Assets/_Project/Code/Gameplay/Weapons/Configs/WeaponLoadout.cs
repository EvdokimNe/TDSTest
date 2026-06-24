using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "TDS/Weapons/Loadout")]
    public sealed class WeaponLoadout : ScriptableObject
    {
        public List<WeaponConfig> Weapons = new();
    }
}
