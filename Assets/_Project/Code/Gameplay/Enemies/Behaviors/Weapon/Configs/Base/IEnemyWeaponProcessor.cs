using System;

namespace _Project.Code.Gameplay.Enemies.Behaviors.Weapon
{
    public interface IEnemyWeaponProcessor
    {
        Type ConfigType { get; }
        void Tick(EnemyWeaponContext ctx);
    }
}
