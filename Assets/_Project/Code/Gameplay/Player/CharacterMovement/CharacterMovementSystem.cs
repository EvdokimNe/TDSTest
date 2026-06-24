using _Project.Code.Gameplay.Input;
using UnityEngine;
namespace _Project.Code.Gameplay.Player.CharacterMovement
{
    public sealed class CharacterMovementSystem
    {
        private readonly InputService _input;
        private readonly PlayerProvider _player;

        public CharacterMovementSystem(InputService input, PlayerProvider playerProvider)
        {
            _input = input;
            _player = playerProvider;
        }

        public void Tick(float deltaTime)
        {
            var direction = new Vector3(_input.Move.x, 0f, _input.Move.y);
            if (direction.sqrMagnitude > 1f)
                direction.Normalize();

            _player.Motor.Move(direction, deltaTime);
        }
    }
}
