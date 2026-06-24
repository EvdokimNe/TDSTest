using System;
using _Project.Code.Gameplay.UI;
using _Project.Code.Gameplay.UI.Tooltips;
using R3;
using Object = UnityEngine.Object;

namespace _Project.Code.Gameplay.Player.UI
{
    public sealed class PlayerHudMediator : IDisposable
    {
        private readonly PlayerProvider _player;
        private readonly PlayerHudView _hudPrefab;
        private readonly TooltipService _tooltipService;
        private readonly UiCanvasLayersProvider _canvasLayersProvider;

        private PlayerHudView _view;
        private IDisposable _subscription;

        public PlayerHudMediator(
            PlayerProvider player,
            PlayerHudView hudPrefab,
            TooltipService tooltipService,
            UiCanvasLayersProvider canvasLayersProvider)
        {
            _player = player;
            _hudPrefab = hudPrefab;
            _tooltipService = tooltipService;
            _canvasLayersProvider = canvasLayersProvider;
        }

        public void Initialize()
        {
            _view = Object.Instantiate(_hudPrefab, _canvasLayersProvider.UiLayerCanvas.transform, false);
            _view.TooltipTrigger.Init(_tooltipService, new PlayerHealthToken());

            var health = _player.CombatEntityState.Health;
            _subscription = health.Current.Subscribe(current => _view.SetHealth(current, health.Max));
        }

        public void Dispose()
        {
            _subscription?.Dispose();

            if (_view != null)
                Object.Destroy(_view.gameObject);
        }
    }
}
