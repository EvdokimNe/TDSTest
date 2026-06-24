using _Project.Code.Gameplay.Paths.Contracts;
using _Project.Code.Gameplay.Paths.Models;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace _Project.Code.Gameplay.Paths.Runtime
{
    public sealed class UnitySplinePath : ISplinePath
    {
        private readonly SplineContainer _container;
        private readonly float _length;

        public UnitySplinePath(SplineContainer container)
        {
            _container = container;
            _length = _container.CalculateLength();
        }

        public bool TryEvaluate(float distance, out SplinePathSample sample)
        {
            sample = default;
            if (_container == null) return false;

            var pathDistance = Mathf.Clamp(distance, 0f, _length);
            var progress = Mathf.Clamp01(pathDistance / _length);

            if (!_container.Evaluate(progress, out var position, out var tangent, out _)) return false;

            sample = new SplinePathSample(
                ToVector3(position),
                ToVector3(tangent).normalized,
                pathDistance,
                distance >= _length);

            return true;
        }

        private static Vector3 ToVector3(float3 value) => new(value.x, value.y, value.z);
    }
}
