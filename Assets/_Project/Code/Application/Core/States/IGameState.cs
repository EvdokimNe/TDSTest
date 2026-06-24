using System.Threading;
using Cysharp.Threading.Tasks;

namespace _Project.Code.Application.Core.States
{
    public interface IGameState
    {
        UniTask Enter(CancellationToken ct);
        UniTask Exit(CancellationToken ct);
    }
}
