using System.Threading;
using _Project.Code.Application.Core.States;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace _Project.Code.Application.Core
{
    public class CoreEntryPoint : IAsyncStartable
    {
        private readonly GameStateMachine _stateMachine;

        public CoreEntryPoint(GameStateMachine stateMachine) => _stateMachine = stateMachine;

        public UniTask StartAsync(CancellationToken cancellation) =>
            _stateMachine.ChangeState<GameplayState>(cancellation);
    }
}
