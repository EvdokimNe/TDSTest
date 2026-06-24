using UnityEngine;

namespace _Project.Code.Gameplay.Paths.Models
{
    public readonly struct SplinePathSample
    {
        public readonly Vector3 Position;
        public readonly Vector3 Forward;
        public readonly float Distance;
        public readonly bool IsComplete;

        public SplinePathSample(Vector3 position, Vector3 forward, float distance, bool isComplete)
        {
            Position = position;
            Forward = forward;
            Distance = distance;
            IsComplete = isComplete;
        }
    }
}
