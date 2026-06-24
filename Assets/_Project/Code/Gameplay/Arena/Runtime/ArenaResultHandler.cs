using _Project.Code.Application.Core.States;
using _Project.Code.Gameplay.UI.Infrastructure;
using _Project.Code.Gameplay.UI.VictoryDefeat;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class ArenaResultHandler : IStartable
    {
        private readonly ArenaResultStream _resultStream;
        private readonly ScreenManager _screenManager;
        private readonly GameStateMachine _stateMachine;

        public ArenaResultHandler(
            ArenaResultStream resultStream,
            ScreenManager screenManager,
            GameStateMachine stateMachine)
        {
            _resultStream = resultStream;
            _screenManager = screenManager;
            _stateMachine = stateMachine;
        }

        private bool _handled;

        public void Start() => _resultStream.Finished += OnResult;

        private void OnResult(ArenaResult result)
        {
            if (_handled) return;
            _handled = true;

            HandleAsync(result).Forget();
        }

        private async UniTaskVoid HandleAsync(ArenaResult result)
        {
            await _screenManager.ShowAndAwaitAsync<VictoryDefeatScreen, VictoryDefeatArgs>(
                new VictoryDefeatArgs(result == ArenaResult.Victory));

            _stateMachine.ChangeState<GameplayState>().Forget();
        }
    }
}
