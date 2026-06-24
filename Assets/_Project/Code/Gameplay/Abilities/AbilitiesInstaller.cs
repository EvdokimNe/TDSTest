using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Abilities
{
    public sealed class AbilitiesInstaller : MonoBehaviour
    {
        [SerializeField] private AbilityLoadout _loadout;
        [SerializeField] private AbilitiesPanelView _abilitiesPanelPrefab;
        [SerializeField] private AoeIndicatorView _aoeIndicator;
        [SerializeField] private CastHintView _castHint;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadout);
            builder.RegisterInstance(_abilitiesPanelPrefab);
            builder.RegisterInstance(_aoeIndicator);
            builder.RegisterInstance(_castHint);

            builder.Register<AbilitySystem>(Lifetime.Singleton);

            builder.Register<DashAbilityProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AoeAbilityProcessor>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<AbilityHudMediator>().AsSelf();
        }
    }
}
