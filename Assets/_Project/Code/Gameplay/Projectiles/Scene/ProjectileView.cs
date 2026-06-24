using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileView : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.25f;

        public float Radius => _radius;

#if UNITY_EDITOR
        [SerializeField] private Color _radiusGizmoColor = Color.yellow;
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = _radiusGizmoColor;
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
#endif
    }
}
