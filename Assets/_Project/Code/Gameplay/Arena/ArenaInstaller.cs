using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class ArenaInstaller : MonoBehaviour
    {
        [SerializeField] private ArenaProvider _arenaProvider;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_arenaProvider);

            builder.Register<ArenaResultStream>(Lifetime.Singleton);
            builder.Register<ArenaController>(Lifetime.Singleton);
            builder.Register<ArenaVictoryService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ArenaResultHandler>();

            builder.Register<ImmediateSpawnProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimedSpawnProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
