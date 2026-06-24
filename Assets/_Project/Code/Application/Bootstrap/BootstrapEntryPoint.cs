using _Project.Code.Application.Core;
using _Project.Code.Application.Loading;
using VContainer.Unity;
namespace _Project.Code.Application.Bootstrap
{
    public class BootstrapEntryPoint : IStartable
    {
        private readonly LoadingScreenController _loadingScreenController;
        private readonly CoreLifetimeScope _coreLifetimeScope;
        
        public BootstrapEntryPoint(LoadingScreenController loadingScreenController, CoreLifetimeScope coreLifetimeScope)
        {
            _loadingScreenController = loadingScreenController;
            _coreLifetimeScope = coreLifetimeScope;
        }
        
        public void Start()
        {
            _loadingScreenController.Start();
            _loadingScreenController.SetProgress(0f);
            _coreLifetimeScope.Build();
        }
    }
}
