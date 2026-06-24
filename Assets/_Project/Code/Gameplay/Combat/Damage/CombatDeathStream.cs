using System;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class CombatDeathStream
    {
        public event Action<ICombatEntity> Died;

        public void Publish(ICombatEntity entity) => Died?.Invoke(entity);
    }
}
