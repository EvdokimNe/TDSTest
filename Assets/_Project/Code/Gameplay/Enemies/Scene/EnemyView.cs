using System.Collections.Generic;
using _Project.Code.Gameplay.Enemies.Providers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private Color _radiusGizmoColor = Color.red;

        [SerializeField, SerializeReference] private List<IEnemyAdditionalParamsProvider> _additionalParamsProviders;

        public float Radius => _radius;
        public List<IEnemyAdditionalParamsProvider> ParamsProviders => _additionalParamsProviders;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = _radiusGizmoColor;
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
#endif
    }
}
