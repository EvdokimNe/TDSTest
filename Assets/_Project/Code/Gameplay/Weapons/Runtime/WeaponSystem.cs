using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Abilities;
using _Project.Code.Gameplay.Input;
using _Project.Code.Gameplay.MouseAim;
using _Project.Code.Gameplay.Player;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    public sealed class WeaponSystem
    {
        private const float ScrollThreshold = 0.01f;

        private readonly InputService _input;
        private readonly MouseAimService _mouseAim;
        private readonly PlayerProvider _player;
        private readonly AbilitySystem _abilitySystem;
        private readonly WeaponInventory _inventory;
        private readonly Dictionary<Type, IWeaponProcessor> _processors = new();
        private readonly WeaponContext _context = new();

        private int _equippedIndex = -1;

        public WeaponSystem(
            InputService input,
            MouseAimService mouseAim,
            PlayerProvider player,
            AbilitySystem abilitySystem,
            WeaponInventory inventory,
            IEnumerable<IWeaponProcessor> processors)
        {
            _input = input;
            _mouseAim = mouseAim;
            _player = player;
            _abilitySystem = abilitySystem;
            _inventory = inventory;

            foreach (var processor in processors)
                _processors[processor.ConfigType] = processor;
        }

        public void Tick(float deltaTime)
        {
            if (!_inventory.HasWeapons) return;

            var view = _player.WeaponView;
            if (view == null) return;

            HandleSwitch();
            SyncEquip(view);
            _inventory.TickCooldowns(deltaTime);

            if (_abilitySystem.IsCasting) return;
            if (!_input.AttackPressed) return;

            var weapon = _inventory.Current;
            if (!weapon.IsReady) return;

            Fire(weapon, view);
        }

        private void HandleSwitch()
        {
            var scrollY = _input.Scroll.y;
            if (scrollY > ScrollThreshold)
                _inventory.Next();
            else if (scrollY < -ScrollThreshold)
                _inventory.Previous();
        }

        private void SyncEquip(PlayerWeaponView view)
        {
            if (_equippedIndex == _inventory.CurrentIndex) return;

            _equippedIndex = _inventory.CurrentIndex;
            view.Equip(_equippedIndex);
        }

        private void Fire(WeaponRuntime weapon, PlayerWeaponView view)
        {
            var origin = _player.PlayerView.transform.position;
            var muzzle = view.MuzzlePosition;

            var aim = origin + _player.PlayerView.transform.forward;
            if (_mouseAim.TryGetWorldPoint(out var cursor))
                aim = cursor;

            var fromOrigin = aim - origin;
            fromOrigin.y = 0f;

            var fromMuzzle = aim - muzzle;
            fromMuzzle.y = 0f;

            // Целимся от дула к курсору, чтобы пуля прошла через точку прицела.
            // Если курсор оказался позади дула (бег назад) — откат на центр→курсор, иначе вектор развернётся.
            var direction = Vector3.Dot(fromMuzzle, fromOrigin) > 0f ? fromMuzzle : fromOrigin;

            if (direction.sqrMagnitude < 0.0001f)
                direction = _player.PlayerView.transform.forward;
            else
                direction.Normalize();

            _context.Source = _player.Combat;
            _context.Origin = origin;
            _context.AimPoint = aim;
            _context.AimDirection = direction;
            _context.MuzzlePosition = muzzle;

            if (_processors.TryGetValue(weapon.Config.GetType(), out var processor))
                processor.Fire(weapon.Config, _context);

            view.PlayAttack(weapon.Config.AnimationTrigger);
            weapon.CooldownRemaining = weapon.Config.Cooldown;
        }
    }
}
