using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Gameplay.PlayerMovement;
using _Project.Code.Gameplay.Weapons;

namespace _Project.Code.Gameplay.Player
{
    public sealed class PlayerProvider
    {
        public PlayerView PlayerView { get; internal set; }
        public MovementMotor Motor { get; internal set; }
        public ICombatEntity Combat { get; internal set; }
        public CombatEntityState CombatEntityState { get; internal set; }
        public HittableModule Hittable { get; internal set; }
        public PlayerWeaponView WeaponView { get; internal set; }
    }
}
