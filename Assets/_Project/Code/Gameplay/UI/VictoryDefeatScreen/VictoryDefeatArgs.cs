using _Project.Code.Gameplay.UI.Infrastructure;

namespace _Project.Code.Gameplay.UI.VictoryDefeat
{
    public class VictoryDefeatArgs : IScreenArgs
    {
        public readonly bool IsVictory;

        public VictoryDefeatArgs(bool isVictory)
        {
            IsVictory = isVictory;
        }
    }
}
