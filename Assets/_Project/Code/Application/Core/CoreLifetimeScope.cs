using _Project.Code.Application.Core.States;
using _Project.Code.Gameplay.UI;
using _Project.Code.Infrastructure.CamerasProviders;
using _Project.Code.Infrastructure.Configs;
using _Project.Code.Infrastructure.SceneLoading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Code.Application.Core
{
    public class CoreLifetimeScope : LifetimeScope
    {
        [SerializeField] private MainCameraProvider _mainCameraProvider;
        [SerializeField] private UiCameraProvider _uiCameraProvider;
        [SerializeField] private UiCanvasLayersProvider _uiCanvasLayersProvider;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCameras(builder);

            builder.RegisterInstance(_uiCanvasLayersProvider);

            builder.Register<SceneLoadingService>(Lifetime.Singleton);
            builder.Register<ConfigService>(Lifetime.Singleton);

            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);

            builder.RegisterEntryPoint<CoreEntryPoint>();
        }
        
        private void RegisterCameras(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainCameraProvider);
            builder.RegisterInstance(_uiCameraProvider);
        }
    }
}
