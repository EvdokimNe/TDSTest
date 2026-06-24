using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.UI;
using _Project.Code.Infrastructure.CamerasProviders;
using _Project.Code.Shared;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Enemies.HealthBars
{
    public sealed class EnemyHealthBarController : ITickable
    {
        private readonly EnemyRegistry _enemies;
        private readonly CombatEntityRegistry _combat;
        private readonly MainCameraProvider _cameraProvider;
        private readonly UiCameraProvider _uiCameraProvider;
        private readonly UiCanvasLayersProvider _layersProvider;
        private readonly HealthBarPoolProvider _pool;

        private readonly Dictionary<InternalIntId, EnemyHealthBarView> _active = new();
        private readonly List<InternalIntId> _stale = new();

        public EnemyHealthBarController(
            EnemyRegistry enemies,
            CombatEntityRegistry combat,
            MainCameraProvider cameraProvider,
            UiCameraProvider uiCameraProvider,
            UiCanvasLayersProvider layersProvider,
            HealthBarPoolProvider pool)
        {
            _enemies = enemies;
            _combat = combat;
            _cameraProvider = cameraProvider;
            _uiCameraProvider = uiCameraProvider;
            _layersProvider = layersProvider;
            _pool = pool;
        }

        public void Tick()
        {
            var camera = _cameraProvider.Value;
            if (camera == null) return;

            var uiCamera = _uiCameraProvider.Value;
            var layerRect = _layersProvider.HpBarLayerCanvas.transform as RectTransform;

            _stale.Clear();
            foreach (var id in _active.Keys)
                _stale.Add(id);

            var enemies = _enemies.Items;
            for (var i = 0; i < enemies.Count; i++)
            {
                var enemy = enemies[i];

                if (!enemy.RuntimeData.IsAlive) continue;
                if (!_combat.TryGet(enemy, out var state) || state.Health == null) continue;

                var health = state.Health;
                if (health.Current.Value >= health.Max) continue;

                var worldPos = enemy.View.transform.position + Vector3.up * 2;
                var screen = camera.WorldToScreenPoint(worldPos);
                if (screen.z <= 0f) continue;
                if (screen.x < 0f || screen.x > Screen.width || screen.y < 0f || screen.y > Screen.height) continue;

                if (!RectTransformUtility.ScreenPointToWorldPointInRectangle(layerRect, screen, uiCamera, out var worldPoint))
                    continue;

                var bar = GetOrRent(enemy.Id);
                bar.SetWorldPosition(worldPoint);
                bar.SetProgress(health.Max <= 0f ? 0f : health.Current.Value / health.Max);

                _stale.Remove(enemy.Id);
            }

            for (var i = 0; i < _stale.Count; i++)
                Release(_stale[i]);
        }

        private EnemyHealthBarView GetOrRent(InternalIntId id)
        {
            if (_active.TryGetValue(id, out var bar))
                return bar;

            bar = _pool.Rent();
            _active[id] = bar;
            return bar;
        }

        private void Release(InternalIntId id)
        {
            if (!_active.TryGetValue(id, out var bar))
                return;

            _active.Remove(id);
            _pool.Return(bar);
        }
    }
}
