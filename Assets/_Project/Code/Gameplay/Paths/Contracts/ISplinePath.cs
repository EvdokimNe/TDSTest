using _Project.Code.Gameplay.Paths.Models;

namespace _Project.Code.Gameplay.Paths.Contracts
{
    public interface ISplinePath
    {
        bool TryEvaluate(float distance, out SplinePathSample sample);
    }
}
