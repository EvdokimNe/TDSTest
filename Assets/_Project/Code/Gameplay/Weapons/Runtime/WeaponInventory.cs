using System.Collections.Generic;
using R3;

namespace _Project.Code.Gameplay.Weapons
{
    public sealed class WeaponInventory
    {
        private readonly List<WeaponRuntime> _weapons = new();

        public int CurrentIndex { get; private set; }
        public IReadOnlyList<WeaponRuntime> Weapons => _weapons;
        public bool HasWeapons => _weapons.Count > 0;

        public WeaponRuntime Current =>
            _weapons.Count > 0 ? _weapons[CurrentIndex] : null;

        public ReactiveProperty<WeaponRuntime> CurrentWeapon { get; } = new();

        public WeaponInventory(WeaponLoadout loadout)
        {
            foreach (var config in loadout.Weapons)
            {
                if (config != null)
                    _weapons.Add(new WeaponRuntime(config));
            }

            CurrentWeapon.Value = Current;
        }

        public bool Next()
        {
            if (_weapons.Count <= 1) return false;
            CurrentIndex = (CurrentIndex + 1) % _weapons.Count;
            CurrentWeapon.Value = Current;
            return true;
        }

        public bool Previous()
        {
            if (_weapons.Count <= 1) return false;
            CurrentIndex = (CurrentIndex - 1 + _weapons.Count) % _weapons.Count;
            CurrentWeapon.Value = Current;
            return true;
        }

        public void TickCooldowns(float deltaTime)
        {
            for (var i = 0; i < _weapons.Count; i++)
            {
                var weapon = _weapons[i];
                if (weapon.CooldownRemaining > 0f)
                    weapon.CooldownRemaining -= deltaTime;
            }
        }
    }
}
