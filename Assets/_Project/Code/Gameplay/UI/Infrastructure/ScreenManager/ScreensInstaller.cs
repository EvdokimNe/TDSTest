using _Project.Code.Gameplay.UI.VictoryDefeat;
using UnityEngine;
using VContainer;

namespace _Project.Code.Gameplay.UI.Infrastructure
{
    public sealed class ScreensInstaller : MonoBehaviour
    {
        [SerializeField] private VictoryDefeatView _victoryDefeatView;

        public void Install(IContainerBuilder builder)
        {
            var catalog = new ScreenCatalog();
            catalog.Add<VictoryDefeatScreen>(_victoryDefeatView);
            builder.RegisterInstance(catalog);

            builder.Register<VictoryDefeatScreen>(Lifetime.Transient);
            builder.Register<ScreenManager>(Lifetime.Singleton);
        }
    }
}
