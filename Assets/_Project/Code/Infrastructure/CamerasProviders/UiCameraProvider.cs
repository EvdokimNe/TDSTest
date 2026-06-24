using UnityEngine;
namespace _Project.Code.Infrastructure.CamerasProviders
{
    public class UiCameraProvider : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public Camera Value => _camera;
    }
}
