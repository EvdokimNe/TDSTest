using _Project.Code.Gameplay.Abilities;
using _Project.Code.Gameplay.Arena;
using _Project.Code.Gameplay.CameraFollow;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Despawn;
using _Project.Code.Gameplay.Enemies;
using _Project.Code.Gameplay.Enemies.HealthBars;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Gameplay.Input;
using _Project.Code.Gameplay.MouseAim;
using _Project.Code.Gameplay.Pause;
using _Project.Code.Gameplay.Player.UI;
using _Project.Code.Gameplay.PlayerMovement;
using _Project.Code.Gameplay.Projectiles;
using _Project.Code.Gameplay.UI;
using _Project.Code.Gameplay.UI.Cursor;
using _Project.Code.Gameplay.UI.Infrastructure;
using _Project.Code.Gameplay.UI.Tooltips;
using _Project.Code.Gameplay.Timers;
using _Project.Code.Gameplay.Weapons;
using _Project.Code.Shared;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay
{
    public sealed class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerMovementInstaller _playerMovementInstaller;
        [SerializeField] private PlayerUiInstaller _playerUiInstaller;
        [SerializeField] private ArenaInstaller _arenaInstaller;
        [SerializeField] private AbilitiesInstaller _abilitiesInstaller;
        [SerializeField] private WeaponsInstaller _weaponsInstaller;
        [SerializeField] private HealthBarsInstaller _healthBarsInstaller;
        [SerializeField] private ScreensInstaller _screensInstaller;
        [SerializeField] private TooltipInstaller _tooltipInstaller;
        [SerializeField] private CursorInstaller _cursorInstaller;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InternalIntIdProvider>(Lifetime.Singleton);
            builder.Register<PauseManager>(Lifetime.Singleton);

            builder.Register<InputService>(Lifetime.Singleton);
            builder.Register<MouseAimService>(Lifetime.Singleton);
            builder.Register<CameraFollowSystem>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            new CombatInstaller().Install(builder);
            new HitDetectionInstaller().Install(builder);
            new EnemyInstaller().Install(builder);
            new TimerInstaller().Install(builder);
            new ProjectileInstaller().Install(builder);
            
            _playerMovementInstaller.Install(builder);
            _playerUiInstaller.Install(builder);
            _arenaInstaller.Install(builder);
            _abilitiesInstaller.Install(builder);
            _weaponsInstaller.Install(builder);
            _healthBarsInstaller.Install(builder);
            _screensInstaller.Install(builder);
            _tooltipInstaller.Install(builder);
            _cursorInstaller.Install(builder);

            builder.Register<HudController>(Lifetime.Singleton);

            builder.Register<DespawnSystem>(Lifetime.Singleton);

            builder.RegisterEntryPoint<GameplayRunner>();
        }
    }
}
