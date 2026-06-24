using VContainer;

namespace _Project.Code.Gameplay.Timers
{
    public sealed class TimerInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<TimerRunner>(Lifetime.Singleton);
        }
    }
}
