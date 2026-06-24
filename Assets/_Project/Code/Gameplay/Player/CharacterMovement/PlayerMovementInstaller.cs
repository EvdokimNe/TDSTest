using _Project.Code.Gameplay.Player;
using _Project.Code.Gameplay.Player.CharacterMovement;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.PlayerMovement
{
    public sealed class PlayerMovementInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private InputActionAsset _inputActionAsset;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_playerView);
            builder.RegisterInstance(_inputActionAsset);

            builder.Register<PlayerProvider>(Lifetime.Singleton);
            builder.Register<PlayerFactory>(Lifetime.Singleton);

            builder.Register<CharacterMovementSystem>(Lifetime.Singleton);
            builder.Register<CharacterRotationSystem>(Lifetime.Singleton);

            builder.RegisterEntryPoint<PlayerDeathHandler>();
        }
    }
}
