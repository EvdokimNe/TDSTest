using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileRuntimeData
    {
        public Vector3 Direction { get; set; }
        public DamagePayload DamagePayload { get; }
        public float LifetimeRemaining { get; set; }
        public float SpeedMultiplier { get; set; }
        public float Radius { get; set; }
        public bool IsAlive { get; set; }

        public ProjectileRuntimeData(Vector3 direction, DamagePayload damagePayload, float lifetime, float radius)
        {
            Direction = direction;
            DamagePayload = damagePayload;
            LifetimeRemaining = lifetime;
            Radius = radius;
            SpeedMultiplier = 1f;
            IsAlive = true;
        }
    }
}
