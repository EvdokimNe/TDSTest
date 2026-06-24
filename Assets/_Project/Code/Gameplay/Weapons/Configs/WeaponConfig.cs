using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    public abstract class WeaponConfig : ScriptableObject
    {
        public string DisplayName;
        public float Cooldown = 0.5f;
        public string AnimationTrigger;
        public AttackConfig AttackConfig;
    }
}
