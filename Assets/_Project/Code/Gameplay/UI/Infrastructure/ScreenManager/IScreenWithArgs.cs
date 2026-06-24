namespace _Project.Code.Gameplay.UI.Infrastructure
{
    public interface IScreenWithArgs<in TArgs> where TArgs : IScreenArgs
    {
        void SetArgs(TArgs args);
    }
}
