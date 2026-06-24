using _Project.Code.Gameplay.Paths.Contracts;
using _Project.Code.Gameplay.Paths.Runtime;
using UnityEngine;
using UnityEngine.Splines;

namespace _Project.Code.Gameplay.Paths.Scene
{
    public sealed class SplinePathProvider : MonoBehaviour
    {
        [SerializeField] private SplineContainer _spline;

        private UnitySplinePath _path;

        public bool TryGetPath(out ISplinePath path)
        {
            path = null;
            if (_spline == null) return false;

            _path ??= new UnitySplinePath(_spline);
            path = _path;
            return true;
        }

        private void Reset() => _spline = GetComponent<SplineContainer>();
    }
}
