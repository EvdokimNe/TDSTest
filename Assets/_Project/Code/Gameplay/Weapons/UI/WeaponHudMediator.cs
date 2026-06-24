using System;
using _Project.Code.Gameplay.UI;
using R3;
using Object = UnityEngine.Object;

namespace _Project.Code.Gameplay.Weapons.UI
{
    public sealed class WeaponHudMediator : IDisposable
    {
        private readonly WeaponInventory _inventory;
        private readonly WeaponHudView _hudPrefab;
        private readonly UiCanvasLayersProvider _canvasLayersProvider;

        private WeaponHudView _view;
        private IDisposable _subscription;

        public WeaponHudMediator(
            WeaponInventory inventory,
            WeaponHudView hudPrefab,
            UiCanvasLayersProvider canvasLayersProvider)
        {
            _inventory = inventory;
            _hudPrefab = hudPrefab;
            _canvasLayersProvider = canvasLayersProvider;
        }

        public void Initialize()
        {
            _view = Object.Instantiate(_hudPrefab, _canvasLayersProvider.UiLayerCanvas.transform, false);
            _subscription = _inventory.CurrentWeapon.Subscribe(weapon =>
                _view.SetWeapon(weapon != null ? weapon.Config.DisplayName : "—"));
        }

        public void Dispose()
        {
            _subscription?.Dispose();

            if (_view != null)
                Object.Destroy(_view.gameObject);
        }
    }
}
