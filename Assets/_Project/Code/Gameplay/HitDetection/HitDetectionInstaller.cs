using VContainer;

namespace _Project.Code.Gameplay.HitDetection
{
    public sealed class HitDetectionInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<HittableRegistry>(Lifetime.Singleton);
            builder.Register<HitQueryService>(Lifetime.Singleton);
        }
    }
}
