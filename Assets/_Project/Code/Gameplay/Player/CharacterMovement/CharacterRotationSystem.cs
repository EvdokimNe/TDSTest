using _Project.Code.Gameplay.MouseAim;
using _Project.Code.Gameplay.PlayerMovement;
using UnityEngine;
namespace _Project.Code.Gameplay.Player.CharacterMovement
{
    public class CharacterRotationSystem
    {
        private readonly PlayerView _view;
        private readonly MouseAimService _mouseAimService;

        public CharacterRotationSystem(PlayerView view, MouseAimService mouseAimService)
        {
            _view = view;
            _mouseAimService = mouseAimService;
        }

        public void Tick(float deltaTime)
        {
            RotateToAimPoint();
        }

        private void RotateToAimPoint()
        {
            if (!_mouseAimService.TryGetWorldPoint(out var aimPoint)) return;

            var direction = aimPoint - _view.transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f) return;

            _view.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
