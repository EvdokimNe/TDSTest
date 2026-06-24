using UnityEngine;

namespace _Project.Code.Gameplay.PlayerMovement
{
    public sealed class MovementMotor
    {
        private readonly CharacterController _characterController;
        private readonly float _moveSpeed;

        private Vector3 _forcedDirection;
        private float _forcedSpeed;
        private float _forcedTimeLeft;

        public MovementMotor(CharacterController characterController, float moveSpeed)
        {
            _characterController = characterController;
            _moveSpeed = moveSpeed;
        }

        public bool IsForcedMoveActive => _forcedTimeLeft > 0f;

        public void Move(Vector3 direction, float deltaTime)
        {
            if (IsForcedMoveActive)
            {
                TickForcedMove(deltaTime);
                return;
            }

            direction.y = 0f;
            _characterController.Move(direction * _moveSpeed * deltaTime);
        }

        public bool TryStartForcedMove(Vector3 direction, float distance, float duration)
        {
            if (IsForcedMoveActive) return false;
            if (direction.sqrMagnitude <= 0f) return false;
            if (distance <= 0f) return false;
            if (duration <= 0f) return false;

            direction.y = 0f;
            direction.Normalize();

            _forcedDirection = direction;
            _forcedSpeed = distance / duration;
            _forcedTimeLeft = duration;
            return true;
        }

        private void TickForcedMove(float deltaTime)
        {
            var moveTime = Mathf.Min(deltaTime, _forcedTimeLeft);
            _characterController.Move(_forcedDirection * _forcedSpeed * moveTime);
            _forcedTimeLeft -= deltaTime;
        }
    }
}
