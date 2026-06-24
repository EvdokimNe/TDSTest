using System;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Movement
{
    public interface IEnemyMovementProcessor
    {
        Type ConfigType { get; }
        void Tick(EnemyMovementContext ctx);
    }
}
