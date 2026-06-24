using _Project.Code.Gameplay.Weapons.UI;
using UnityEngine;
using VContainer;

namespace _Project.Code.Gameplay.Weapons
{
    public sealed class WeaponsInstaller : MonoBehaviour
    {
        [SerializeField] private WeaponLoadout _loadout;
        [SerializeField] private PlayerWeaponView _weaponViewPrefab;
        [SerializeField] private WeaponHudView _weaponHudPrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadout);
            builder.RegisterInstance(_weaponViewPrefab);
            builder.RegisterInstance(_weaponHudPrefab);

            builder.Register<WeaponInventory>(Lifetime.Singleton);
            builder.Register<WeaponSystem>(Lifetime.Singleton);
            builder.Register<WeaponHudMediator>(Lifetime.Singleton);

            builder.Register<MeleeTargetedWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MeleeAroundWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RangedWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
