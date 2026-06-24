using System;

namespace _Project.Code.Gameplay.Weapons
{
    public interface IWeaponProcessor
    {
        Type ConfigType { get; }
        void Fire(WeaponConfig config, WeaponContext ctx);
    }
}
