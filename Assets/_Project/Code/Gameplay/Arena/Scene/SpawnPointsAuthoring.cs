using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class SpawnPointsAuthoring : SpawnPlacementAuthoring
    {
        [SerializeField] private Transform[] _points;

        private int _index;

        public override Vector3 NextPoint()
        {
            if (_points == null || _points.Length == 0)
                return transform.position;

            var point = _points[_index % _points.Length];
            _index++;
            return point != null ? point.position : transform.position;
        }

        private void OnDrawGizmosSelected()
        {
            if (_points == null) return;
            Gizmos.color = Color.cyan;
            foreach (var p in _points)
            {
                if (p != null)
                    Gizmos.DrawSphere(p.position, 0.3f);
            }
        }
    }
}
