using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Code.Gameplay.Abilities
{
    public sealed class AbilitySlot
    {
        private readonly ReactiveProperty<AbilityPhase> _phase = new(AbilityPhase.Ready);

        public AbilityConfig Config { get; }
        public InputActionReference Input { get; }
        public AbilityState State { get; }

        public ReadOnlyReactiveProperty<AbilityPhase> Phase => _phase;

        public float Cooldown01 => Config.Cooldown <= 0f
            ? 0f
            : Mathf.Clamp01(State.CooldownRemaining / Config.Cooldown);

        public bool IsReady => State.CooldownRemaining <= 0f;

        public AbilitySlot(AbilityConfig config, InputActionReference input)
        {
            Config = config;
            Input = input;
            State = config.CreateState();
        }

        public void SetPhase(AbilityPhase phase)
        {
            if (_phase.Value != phase)
                _phase.Value = phase;
        }
    }
}
