using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace _Project.Code.Gameplay.Input
{
    public sealed class InputService
    {
        private const string MapName = "Player";

        private const string MoveActionName = "Move";
        private const string MousePositionActionName = "MousePosition";
        private const string AttackActionName = "Attack";
        private const string ConfirmActionName = "Confirm";
        private const string CancelActionName = "Cancel";

        private readonly InputActionMap _map;

        private readonly InputAction _move;
        private readonly InputAction _mousePosition;
        private readonly InputAction _attack;

        private readonly Dictionary<string, InputAction> _actionCache = new();

        public InputService(InputActionAsset inputActionAsset)
        {
            _map = inputActionAsset.FindActionMap(MapName, true);
            _move = _map.FindAction(MoveActionName, true);
            _mousePosition = _map.FindAction(MousePositionActionName, true);
            _attack = _map.FindAction(AttackActionName, false);

            _map.Enable();
        }

        public Vector2 Move => _move.ReadValue<Vector2>();
        public Vector2 MousePosition => _mousePosition.ReadValue<Vector2>();
        public bool AttackPressed => _attack != null && _attack.IsPressed();

        public bool ConfirmPressed => WasActionPressedThisFrame(ConfirmActionName);
        public bool CancelPressed => WasActionPressedThisFrame(CancelActionName);

        public Vector2 Scroll => Mouse.current != null ? Mouse.current.scroll.ReadValue() : Vector2.zero;

        public bool IsAltHeld => Keyboard.current?.altKey.isPressed ?? false;

        public bool WasActionPressedThisFrame(string actionName)
        {
            var action = GetAction(actionName);
            return action != null && action.WasPressedThisFrame();
        }

        private InputAction GetAction(string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
                return null;

            if (_actionCache.TryGetValue(actionName, out var action))
                return action;

            action = _map.FindAction(actionName, false);
            _actionCache[actionName] = action;
            return action;
        }
    }
}
