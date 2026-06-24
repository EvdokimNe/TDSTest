using System.Threading;
using _Project.Code.Application.Loading;
using _Project.Code.Infrastructure.Configs;
using _Project.Code.Infrastructure.SceneLoading;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Application.Core.States
{
    public sealed class GameplayState : IGameState
    {
        private readonly ConfigService _configService;
        private readonly SceneLoadingService _sceneLoadingService;
        private readonly LoadingScreenController _loadingScreen;

        private bool _nextAttemptFails = UnityEngine.Random.value > 0.5f;

        public GameplayState(
            ConfigService configService,
            SceneLoadingService sceneLoadingService,
            LoadingScreenController loadingScreen)
        {
            _configService = configService;
            _sceneLoadingService = sceneLoadingService;
            _loadingScreen = loadingScreen;
        }

        public async UniTask Enter(CancellationToken ct)
        {
            _loadingScreen.SetStatusText("Загрузка");
            _loadingScreen.Start();

            await RunFakeConnectionAsync(ct);

            _loadingScreen.SetStatusText("Загрузка конфигов...");
            _loadingScreen.SetProgress(0f);
            await _configService.LoadAsync(ConfigGroups.Gameplay);

            _loadingScreen.SetStatusText("Загрузка сцены...");
            await _sceneLoadingService.LoadSceneAsync(SceneNames.Gameplay, v =>
            {
                _loadingScreen.SetProgress(v);
            });

            _loadingScreen.SetProgress(1f);
            await UniTask.Delay(100, cancellationToken: ct);
            _loadingScreen.Stop();
        }

        public UniTask Exit(CancellationToken ct)
        {
            _configService.Release(ConfigGroups.Gameplay);
            return UniTask.CompletedTask;
        }

        private async UniTask RunFakeConnectionAsync(CancellationToken ct)
        {
            _loadingScreen.SetStatusText("Соединение с сервером...");
            _loadingScreen.SetProgress(0f);
            await UniTask.Delay(1500, cancellationToken: ct);
           
            if (_nextAttemptFails)
            {
                _loadingScreen.ShowRetryButton("Ошибка соединения. Повторить попытку?");
                await _loadingScreen.WaitForRetryAsync();

                _loadingScreen.SetStatusText("Соединение с сервером...");
                _loadingScreen.SetProgress(0f);
                await UniTask.Delay(1500, cancellationToken: ct);
            }

            _nextAttemptFails = !_nextAttemptFails;
        }
    }
}
