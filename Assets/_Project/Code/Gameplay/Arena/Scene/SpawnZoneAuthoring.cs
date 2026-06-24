using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class SpawnZoneAuthoring : SpawnPlacementAuthoring
    {
        [SerializeField] private Vector2 _size = new(10f, 10f);
        [SerializeField] private float _yLevel;

        public override Vector3 NextPoint()
        {
            var center = transform.position;
            var halfX = _size.x * 0.5f;
            var halfZ = _size.y * 0.5f;

            return new Vector3(
                Random.Range(center.x - halfX, center.x + halfX),
                _yLevel,
                Random.Range(center.z - halfZ, center.z + halfZ));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                new Vector3(transform.position.x, _yLevel, transform.position.z),
                new Vector3(_size.x, 0.01f, _size.y));
        }
    }
}
