using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    public sealed class TooltipInstaller : MonoBehaviour
    {
        [SerializeField] private TooltipView _tooltipPrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_tooltipPrefab);

            builder.Register<TooltipService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<AbilitySlotTooltipProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerHealthTooltipProcessor>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
