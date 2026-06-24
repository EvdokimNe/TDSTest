using _Project.Code.Gameplay.Combat;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Player
{
    public sealed class PlayerCombatEntity : ICombatEntity
    {
        public InternalIntId Id { get; }
        public CombatTeam Team => CombatTeam.Player;

        public PlayerCombatEntity(InternalIntId id)
        {
            Id = id;
        }
    }
}
