using System.Collections.Generic;
using _Project.Code.Gameplay.Pause;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace _Project.Code.Gameplay.UI.Infrastructure
{
    public sealed class ScreenManager
    {
        private readonly ScreenCatalog _catalog;
        private readonly IObjectResolver _resolver;
        private readonly UiCanvasLayersProvider _layers;
        private readonly PauseManager _pauseManager;
        private readonly List<BaseScreen> _active = new();

        public ScreenManager(ScreenCatalog catalog, IObjectResolver resolver, UiCanvasLayersProvider layers, PauseManager pauseManager)
        {
            _catalog = catalog;
            _resolver = resolver;
            _layers = layers;
            _pauseManager = pauseManager;
        }

        public bool HasActiveScreen => _active.Count > 0;

        public UniTask ShowAsync<TScreen>() where TScreen : BaseScreen
        {
            var (screen, view) = Open<TScreen>();
            AwaitCloseAndCleanupAsync(screen, view).Forget();
            return UniTask.CompletedTask;
        }

        public UniTask ShowAsync<TScreen, TArgs>(TArgs args)
            where TScreen : BaseScreen, IScreenWithArgs<TArgs>
            where TArgs : IScreenArgs
        {
            var (screen, view) = Open<TScreen>();
            ((IScreenWithArgs<TArgs>)screen).SetArgs(args);
            AwaitCloseAndCleanupAsync(screen, view).Forget();
            return UniTask.CompletedTask;
        }

        public async UniTask ShowAndAwaitAsync<TScreen>() where TScreen : BaseScreen
        {
            var (screen, view) = Open<TScreen>();
            await AwaitCloseAndCleanupAsync(screen, view);
        }

        public async UniTask ShowAndAwaitAsync<TScreen, TArgs>(TArgs args)
            where TScreen : BaseScreen, IScreenWithArgs<TArgs>
            where TArgs : IScreenArgs
        {
            var (screen, view) = Open<TScreen>();
            ((IScreenWithArgs<TArgs>)screen).SetArgs(args);
            await AwaitCloseAndCleanupAsync(screen, view);
        }

        private (BaseScreen screen, BaseView view) Open<TScreen>() where TScreen : BaseScreen
        {
            var prefab = _catalog.GetPrefab(typeof(TScreen));
            var view = Object.Instantiate(prefab, _layers.UiLayerCanvas.transform);

            var screen = (BaseScreen)_resolver.Resolve(typeof(TScreen));
            screen.BindView(view);

            _active.Add(screen);
            _pauseManager.Pause();
            return (screen, view);
        }

        private async UniTask AwaitCloseAndCleanupAsync(BaseScreen screen, BaseView view)
        {
            await screen.RunAsync();

            _active.Remove(screen);
            _pauseManager.Resume();
            Object.Destroy(view.gameObject);
        }
    }
}
