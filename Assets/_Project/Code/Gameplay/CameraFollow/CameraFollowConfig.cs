using UnityEngine;

namespace _Project.Code.Gameplay.CameraFollow
{
    [CreateAssetMenu(menuName = "TDS/Camera Follow Config")]
    public sealed class CameraFollowConfig : ScriptableObject
    {
        public Vector3 Offset = new(0f, 12f, -8f);
        public float SmoothTime = 0.12f;
        public float MaxSpeed = 100f;
    }
}
