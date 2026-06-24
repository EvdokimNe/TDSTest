using _Project.Code.Gameplay.Combat;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileModificationContext
    {
        public ProjectileAgent Agent { get; private set; }
        public DamagePayload Payload { get; private set; }

        public float SpeedMultiplier { get; set; }
        public float ScaleMultiplier { get; set; }
        public float RadiusMultiplier { get; set; }

        public void Reset(ProjectileAgent agent)
        {
            Agent = agent;
            Payload = agent.RuntimeData.DamagePayload;
            SpeedMultiplier = 1f;
            ScaleMultiplier = 1f;
            RadiusMultiplier = 1f;
        }
    }
}
