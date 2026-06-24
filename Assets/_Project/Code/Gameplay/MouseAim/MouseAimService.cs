using _Project.Code.Gameplay.Input;
using _Project.Code.Infrastructure.CamerasProviders;
//using UGizmo;
using UnityEngine;

namespace _Project.Code.Gameplay.MouseAim
{
    public sealed class MouseAimService
    {
        private static readonly Plane GroundPlane = new(Vector3.up, Vector3.zero);

        private readonly InputService _inputService;
        private readonly MainCameraProvider _mainCameraProvider;

        private int _cachedFrame = -1;
        private bool _hasWorldPoint;
        private Vector2 _cursorScreenPoint;
        private Vector2 _worldScreenPoint;
        private Vector3 _worldPoint;

        public MouseAimService(InputService inputServiceService, MainCameraProvider mainCameraProvider)
        {
            _inputService = inputServiceService;
            _mainCameraProvider = mainCameraProvider;
        }

        public Vector2 CursorScreenPoint
        {
            get
            {
                UpdateCacheIfNeeded();
                return _cursorScreenPoint;
            }
        }

        public bool TryGetWorldPoint(out Vector3 point)
        {
            UpdateCacheIfNeeded();
            point = _worldPoint;
            return _hasWorldPoint;
        }

        public bool TryGetWorldScreenPoint(out Vector2 point)
        {
            UpdateCacheIfNeeded();
            point = _worldScreenPoint;
            return _hasWorldPoint;
        }

        private void UpdateCacheIfNeeded()
        {
            if (_cachedFrame == Time.frameCount) return;

            _cachedFrame = Time.frameCount;
            _cursorScreenPoint = _inputService.MousePosition;
            _hasWorldPoint = false;
            _worldPoint = default;
            _worldScreenPoint = default;

            var camera = _mainCameraProvider.Value;
            var ray = camera.ScreenPointToRay(_cursorScreenPoint);
            
            //UGizmos.DrawRay(ray, Color.yellow);

            if (!GroundPlane.Raycast(ray, out var distance))
                return;

            _hasWorldPoint = true;
            _worldPoint = ray.GetPoint(distance);
            _worldScreenPoint = camera.WorldToScreenPoint(_worldPoint);

            //UGizmos.DrawSphere(_worldPoint, 0.15f, Color.red);
        }
    }
}
