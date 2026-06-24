using _Project.Code.Gameplay.Despawn;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileMovementService
    {
        private readonly ProjectileRegistry _registry;
        private readonly DespawnSystem _despawnSystem;

        public ProjectileMovementService(ProjectileRegistry registry, DespawnSystem despawnSystem)
        {
            _registry = registry;
            _despawnSystem = despawnSystem;
        }

        public void Tick(float deltaTime)
        {
            var items = _registry.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var agent = items[i];
                if (!agent.RuntimeData.IsAlive)
                    continue;

                agent.RuntimeData.LifetimeRemaining -= deltaTime;
                if (agent.RuntimeData.LifetimeRemaining <= 0f)
                {
                    _despawnSystem.RequestProjectile(agent.Id);
                    continue;
                }

                agent.View.transform.position += agent.RuntimeData.Direction * (agent.Config.Speed * agent.RuntimeData.SpeedMultiplier) * deltaTime;
            }
        }
    }
}
