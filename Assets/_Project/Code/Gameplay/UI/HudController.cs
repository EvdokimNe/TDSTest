using _Project.Code.Gameplay.Abilities;
using _Project.Code.Gameplay.Player.UI;
using _Project.Code.Gameplay.Weapons.UI;

namespace _Project.Code.Gameplay.UI
{
    public sealed class HudController
    {
        private readonly PlayerHudMediator _playerHud;
        private readonly AbilityHudMediator _abilityHud;
        private readonly WeaponHudMediator _weaponHud;

        public HudController(
            PlayerHudMediator playerHud,
            AbilityHudMediator abilityHud,
            WeaponHudMediator weaponHud)
        {
            _playerHud = playerHud;
            _abilityHud = abilityHud;
            _weaponHud = weaponHud;
        }

        public void Initialize()
        {
            _playerHud.Initialize();
            _abilityHud.Initialize();
            _weaponHud.Initialize();
        }
    }
}
