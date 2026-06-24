using _Project.Code.Gameplay.Enemies.Behaviors.Movement;
using _Project.Code.Gameplay.Enemies.Behaviors.Weapon;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<EnemyRegistry>(Lifetime.Singleton);
            builder.Register<EnemyPoolProvider>(Lifetime.Singleton);
            builder.Register<EnemyFactory>(Lifetime.Singleton);
            builder.Register<EnemyMovementService>(Lifetime.Singleton);
            builder.Register<EnemyWeaponService>(Lifetime.Singleton);
            builder.Register<EnemyDiedEventStream>(Lifetime.Singleton);
            builder.RegisterEntryPoint<EnemyDeathHandler>();

            builder.Register<SplineMovementProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ChasePlayerMovementProcessor>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<RadialBurstWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AimedShotWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MeleeWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CircleShootWeaponProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
