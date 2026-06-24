using _Project.Code.Application.Core;
using _Project.Code.Application.Loading;
using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace _Project.Code.Application.Bootstrap
{
    public class BootstrapLifetimeScope : LifetimeScope
    {
        [SerializeField] private LoadingScreenView _loadingScreenView;
        [SerializeField] private CoreLifetimeScope _coreLifetimeScope;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreenView);
            builder.Register<LoadingScreenController>(Lifetime.Singleton);
            
            builder.RegisterInstance(_coreLifetimeScope);
            
            builder.RegisterEntryPoint<BootstrapEntryPoint>();
        }
    }
}
