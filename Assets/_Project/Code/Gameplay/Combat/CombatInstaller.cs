using _Project.Code.Shared;
using VContainer;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class CombatInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<CombatEntityRegistry>(Lifetime.Singleton);
            builder.Register<CombatDeathStream>(Lifetime.Singleton);
            builder.Register<DamagePayloadFactory>(Lifetime.Singleton);
            builder.Register<DamagePipeline>(Lifetime.Singleton);
            builder.Register<DamageApplicationService>(Lifetime.Singleton);
            
            builder.Register<CriticalChancePreResolvedModifierProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PercentDamageOnHitModifierProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ZeroDamageModifierProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ArmorDamageModifierProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
