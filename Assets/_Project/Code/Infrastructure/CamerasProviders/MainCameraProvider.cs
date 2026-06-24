using UnityEngine;
namespace _Project.Code.Infrastructure.CamerasProviders
{
    public class MainCameraProvider : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        public Camera Value => _camera;
    }
}
