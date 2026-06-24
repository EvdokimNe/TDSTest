using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Shared;
using uPools;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyAgent : ICombatEntity
    {
        public InternalIntId Id { get; }
        public CombatTeam Team => CombatTeam.Enemy;

        public EnemyView View { get; }
        public EnemyRuntimeData RuntimeData { get; }
        public ObjectPool<EnemyView> Pool { get; }
        public HittableModule Hittable { get; set; }

        public EnemyAgent(
            InternalIntId id,
            EnemyView view,
            EnemyRuntimeData runtimeData,
            ObjectPool<EnemyView> pool)
        {
            Id = id;
            View = view;
            RuntimeData = runtimeData;
            Pool = pool;
        }
    }
}
