using _Project.Code.Shared;
using uPools;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileAgent
    {
        public InternalIntId Id { get; }
        public ProjectileView View { get; }
        public ProjectileConfig Config { get; }
        public ProjectileRuntimeData RuntimeData { get; }
        public ObjectPool<ProjectileView> Pool { get; }

        public ProjectileAgent(
            InternalIntId id,
            ProjectileView view,
            ProjectileConfig config,
            ProjectileRuntimeData runtimeData,
            ObjectPool<ProjectileView> pool)
        {
            Id = id;
            View = view;
            Config = config;
            RuntimeData = runtimeData;
            Pool = pool;
        }
    }
}
