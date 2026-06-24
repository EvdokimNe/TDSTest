using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Input;
using _Project.Code.Gameplay.MouseAim;
using _Project.Code.Gameplay.Player;
using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    public sealed class AbilitySystem
    {
        private readonly InputService _input;
        private readonly MouseAimService _mouseAim;
        private readonly PlayerProvider _player;
        private readonly Dictionary<Type, IAbilityProcessor> _processors = new();
        private readonly List<AbilitySlot> _slots = new();
        private readonly AbilityContext _context = new();

        private AbilitySlot _castingSlot;

        public IReadOnlyList<AbilitySlot> Slots => _slots;
        public bool IsCasting => _castingSlot != null;
        public AbilityConfig CastingConfig => _castingSlot?.Config;
        public Vector3 CurrentAimPoint { get; private set; }

        public AbilitySystem(
            AbilityLoadout loadout,
            InputService input,
            MouseAimService mouseAim,
            PlayerProvider player,
            IEnumerable<IAbilityProcessor> processors)
        {
            _input = input;
            _mouseAim = mouseAim;
            _player = player;

            foreach (var processor in processors)
                _processors[processor.ConfigType] = processor;

            foreach (var entry in loadout.Abilities)
            {
                if (entry.Ability == null || entry.Input == null) continue;

                entry.Input.action.Enable();
                _slots.Add(new AbilitySlot(entry.Ability, entry.Input));
            }
        }

        public void Tick(float deltaTime)
        {
            TickCooldowns(deltaTime);

            if (_castingSlot != null)
            {
                TickTargeting();
                return;
            }

            PollActivations();
        }

        private void TickCooldowns(float deltaTime)
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                if (slot.State.CooldownRemaining <= 0f) continue;

                slot.State.CooldownRemaining -= deltaTime;
                if (slot.State.CooldownRemaining <= 0f)
                {
                    slot.State.CooldownRemaining = 0f;
                    slot.SetPhase(AbilityPhase.Ready);
                }
            }
        }

        private void PollActivations()
        {
            for (var i = 0; i < _slots.Count; i++)
            {
                var slot = _slots[i];
                if (slot.State.CooldownRemaining > 0f) continue;
                if (!slot.Input.action.WasPressedThisFrame()) continue;

                if (slot.Config.RequiresTargeting)
                {
                    _castingSlot = slot;
                    _mouseAim.TryGetWorldPoint(out var aim);
                    CurrentAimPoint = aim;
                    slot.SetPhase(AbilityPhase.Targeting);
                }
                else
                {
                    Execute(slot);
                }

                return;
            }
        }

        private void TickTargeting()
        {
            if (_mouseAim.TryGetWorldPoint(out var aim))
                CurrentAimPoint = aim;

            if (_input.IsAltHeld)
            {
                _castingSlot.SetPhase(AbilityPhase.Ready);
                _castingSlot = null;
                return;
            }

            if (_input.CancelPressed)
            {
                _castingSlot.SetPhase(AbilityPhase.Ready);
                _castingSlot = null;
                return;
            }

            if (_input.ConfirmPressed)
            {
                var slot = _castingSlot;
                _castingSlot = null;
                Execute(slot);
            }
        }

        private void Execute(AbilitySlot slot)
        {
            var origin = _player.PlayerView.transform.position;

            var aim = CurrentAimPoint;
            if (!slot.Config.RequiresTargeting && _mouseAim.TryGetWorldPoint(out var cursor))
                aim = cursor;

            var direction = aim - origin;
            direction.y = 0f;

            _context.Source = _player.Combat;
            _context.Origin = origin;
            _context.AimPoint = aim;
            _context.AimDirection = direction.sqrMagnitude > 0.0001f
                ? direction.normalized
                : _player.PlayerView.transform.forward;

            if (_processors.TryGetValue(slot.Config.GetType(), out var processor))
                processor.Execute(slot.Config, _context);

            slot.State.CooldownRemaining = slot.Config.Cooldown;
            slot.SetPhase(AbilityPhase.OnCooldown);
        }
    }
}
