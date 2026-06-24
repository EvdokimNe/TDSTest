using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer;

namespace _Project.Code.Application.Core.States
{
    public sealed class GameStateMachine
    {
        private readonly IObjectResolver _resolver;
        private IGameState _current;

        public GameStateMachine(IObjectResolver resolver) => _resolver = resolver;

        public async UniTask ChangeState<T>(CancellationToken ct = default) where T : IGameState
        {
            if (_current != null)
                await _current.Exit(ct);

            _current = _resolver.Resolve<T>();
            await _current.Enter(ct);
        }
    }
}
