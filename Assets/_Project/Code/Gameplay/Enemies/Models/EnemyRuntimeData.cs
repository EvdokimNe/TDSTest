using _Project.Code.Gameplay.Enemies.Behaviors.Movement;
using _Project.Code.Gameplay.Enemies.Behaviors.Weapon;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyRuntimeData
    {
        public EnemyConfig Config { get; }
        public bool IsAlive { get; set; }
        public EnemyMovementState MovementState { get; set; }
        public WeaponContext WeaponContext { get; set; }

        public EnemyRuntimeData(EnemyConfig config)
        {
            Config = config;
            IsAlive = true;
        }
    }
}
