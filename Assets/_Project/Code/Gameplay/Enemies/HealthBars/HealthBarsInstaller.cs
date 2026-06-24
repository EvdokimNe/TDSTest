using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.Enemies.HealthBars
{
    public sealed class HealthBarsInstaller : MonoBehaviour
    {
        [SerializeField] private EnemyHealthBarView _barPrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_barPrefab);
            builder.Register<HealthBarPoolProvider>(Lifetime.Singleton);
            
            builder.RegisterEntryPoint<EnemyHealthBarController>();
        }
    }
}
