using UnityEngine;
namespace _Project.Code.Shared.Components
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake() => DontDestroyOnLoad(gameObject);
    }
}
