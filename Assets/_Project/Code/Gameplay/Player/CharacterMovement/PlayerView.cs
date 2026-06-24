using UnityEngine;

namespace _Project.Code.Gameplay.PlayerMovement
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _radius = 0.5f;

        public CharacterController CharacterController => _characterController;
        public float Radius => _radius;
    }
}
