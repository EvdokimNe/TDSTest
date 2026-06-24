using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    public abstract class AbilityConfig : ScriptableObject
    {
        public string DisplayName;
        [TextArea] public string Description;
        public float Cooldown = 3f;

        public abstract bool RequiresTargeting { get; }

        public virtual AbilityState CreateState() => new();
    }
}
