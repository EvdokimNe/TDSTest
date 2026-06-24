using UnityEngine;
using VContainer;

namespace _Project.Code.Gameplay.Player.UI
{
    public sealed class PlayerUiInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerHudView _playerHudPrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerHudPrefab);
            builder.Register<PlayerHudMediator>(Lifetime.Singleton);
        }
    }
}
