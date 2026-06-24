using System;
using System.Collections.Generic;
using _Project.Code.Application.Loading;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Gameplay.Input;
using _Project.Code.Gameplay.MouseAim;
using _Project.Code.Gameplay.UI.Infrastructure;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _Project.Code.Gameplay.UI.Cursor
{
    public sealed class CustomCursorService : IStartable, ITickable, IDisposable
    {
        private const float HoverRadius = 1f;

        private static readonly Color NeutralColor = Color.white;
        private static readonly Color EnemyColor = Color.red;

        private readonly InputService _input;
        private readonly MouseAimService _mouseAim;
        private readonly HitQueryService _hitQuery;
        private readonly ScreenManager _screenManager;
        private readonly LoadingScreenController _loadingScreen;
        private readonly CursorView _cursorView;
        private readonly UiCanvasLayersProvider _canvasLayersProvider;

        private readonly List<HittableModule> _buffer = new();

        private CursorView _reticle;
        private bool _uiModeApplied;

        public CustomCursorService(
            InputService input,
            MouseAimService mouseAim,
            HitQueryService hitQuery,
            ScreenManager screenManager,
            LoadingScreenController loadingScreen,
            CursorView cursorView,
            UiCanvasLayersProvider canvasLayersProvider)
        {
            _input = input;
            _mouseAim = mouseAim;
            _hitQuery = hitQuery;
            _screenManager = screenManager;
            _loadingScreen = loadingScreen;
            _cursorView = cursorView;
            _canvasLayersProvider = canvasLayersProvider;
        }

        public void Start()
        {
            _reticle = Object.Instantiate(_cursorView, _canvasLayersProvider.UiLayerCanvas.transform, false);
            ApplyMode(IsUiMode());
        }

        public void Tick()
        {
            var uiMode = IsUiMode();

            if (uiMode != _uiModeApplied)
                ApplyMode(uiMode);

            if (uiMode) return;

            var screenPoint = _mouseAim.TryGetWorldScreenPoint(out var aimScreen)
                ? aimScreen
                : _input.MousePosition;

            _reticle.UpdatePosition(screenPoint);
            _reticle.SetColor(IsAimingAtEnemy() ? EnemyColor : NeutralColor);
        }

        private bool IsUiMode() => _input.IsAltHeld || _screenManager.HasActiveScreen || _loadingScreen.IsVisible;

        private void ApplyMode(bool uiMode)
        {
            _uiModeApplied = uiMode;

            UnityEngine.Cursor.visible = uiMode;
            _reticle.SetVisible(!uiMode);
        }

        public void Dispose()
        {
            UnityEngine.Cursor.visible = true;

            if (_reticle != null)
                Object.Destroy(_reticle.gameObject);
        }

        private bool IsAimingAtEnemy()
        {
            return false;
            
            if (!_mouseAim.TryGetWorldPoint(out var worldPoint))
                return false;

            _hitQuery.OverlapCircle(worldPoint, HoverRadius, CombatTeam.Player, _buffer);

            for (var i = 0; i < _buffer.Count; i++)
            {
                if (_buffer[i].Entity.Team == CombatTeam.Enemy)
                    return true;
            }

            return false;
        }
    }
}
