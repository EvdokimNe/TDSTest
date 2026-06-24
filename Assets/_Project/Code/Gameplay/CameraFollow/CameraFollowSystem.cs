using _Project.Code.Gameplay.PlayerMovement;
using _Project.Code.Infrastructure.CamerasProviders;
using _Project.Code.Infrastructure.Configs;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Code.Gameplay.CameraFollow
{
    public sealed class CameraFollowSystem
    {
        private readonly MainCameraProvider _mainCameraProvider;
        private readonly PlayerView _playerView;
        private readonly ConfigService _configService;

        private CameraFollowConfig _config;
        private Vector3 _velocity;
        private bool _init;

        public CameraFollowSystem(MainCameraProvider mainCameraProvider, PlayerView playerView, ConfigService configService)
        {
            _mainCameraProvider = mainCameraProvider;
            _playerView = playerView;
            _configService = configService;
        }

        public void Init()
        {
            if (_init)
            {
                return;
            }
            _init = true;
            
            if (!_configService.TryGet(out _config))
            {
                Debug.LogError("[CameraFollowSystem] CameraFollowConfig is not loaded.");
                return;
            }

            _mainCameraProvider.Value.transform.position = _playerView.transform.position + _config.Offset;
        }

        public void Tick(float deltaTime)
        {
            Init();
            
            var cameraTransform = _mainCameraProvider.Value.transform;
            var targetPosition = _playerView.transform.position + _config.Offset;

            cameraTransform.position = Vector3.SmoothDamp(
                cameraTransform.position,
                targetPosition,
                ref _velocity,
                _config.SmoothTime,
                _config.MaxSpeed,
                deltaTime);
        }
    }
}
