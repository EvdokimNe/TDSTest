using VContainer;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ProjectileRegistry>(Lifetime.Singleton);
            builder.Register<ProjectilePoolProvider>(Lifetime.Singleton);
            builder.Register<ProjectileModificationPipeline>(Lifetime.Singleton);
            builder.Register<ProjectileFactory>(Lifetime.Singleton);
            builder.Register<ProjectileMovementService>(Lifetime.Singleton);
            builder.Register<ProjectileHitService>(Lifetime.Singleton);

            builder.Register<CriticalProjectileModifierProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
