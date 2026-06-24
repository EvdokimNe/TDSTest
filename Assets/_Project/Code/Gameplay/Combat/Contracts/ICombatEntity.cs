using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Combat
{
    public interface ICombatEntity
    {
        InternalIntId Id { get; }
        CombatTeam Team { get; }
    }
}
