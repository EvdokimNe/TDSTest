using _Project.Code.Gameplay.Abilities;
using _Project.Code.Gameplay.Arena;
using _Project.Code.Gameplay.CameraFollow;
using _Project.Code.Gameplay.Despawn;
using _Project.Code.Gameplay.Enemies;
using _Project.Code.Gameplay.Pause;
using _Project.Code.Gameplay.Player;
using _Project.Code.Gameplay.Player.CharacterMovement;
using _Project.Code.Gameplay.Projectiles;
using _Project.Code.Gameplay.Timers;
using _Project.Code.Gameplay.UI;
using _Project.Code.Gameplay.Weapons;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Code.Gameplay
{
    public sealed class GameplayRunner : IStartable, ITickable
    {
        private readonly PlayerFactory _playerFactory;
        private readonly HudController _hudController;
        private readonly PauseManager _pauseManager;
        private readonly CharacterMovementSystem _characterMovementSystem;
        private readonly CharacterRotationSystem _characterRotationSystem;
        private readonly CameraFollowSystem _cameraFollowSystem;
        private readonly AbilitySystem _abilitySystem;
        private readonly WeaponSystem _weaponSystem;
        private readonly ArenaController _arenaController;
        private readonly ArenaVictoryService _arenaVictoryService;
        private readonly EnemyMovementService _enemyMovementService;
        private readonly EnemyWeaponService _enemyWeaponService;
        private readonly TimerRunner _timerRunner;
        private readonly ProjectileMovementService _projectileMovementService;
        private readonly ProjectileHitService _projectileHitService;
        private readonly DespawnSystem _despawnSystem;

        public GameplayRunner(
            PlayerFactory playerFactory,
            HudController hudController,
            PauseManager pauseManager,
            CharacterMovementSystem characterMovementSystem,
            CharacterRotationSystem characterRotationSystem,
            CameraFollowSystem cameraFollowSystem,
            AbilitySystem abilitySystem,
            WeaponSystem weaponSystem,
            ArenaController arenaController,
            ArenaVictoryService arenaVictoryService,
            EnemyMovementService enemyMovementService,
            EnemyWeaponService enemyWeaponService,
            TimerRunner timerRunner,
            ProjectileMovementService projectileMovementService,
            ProjectileHitService projectileHitService,
            DespawnSystem despawnSystem)
        {
            _playerFactory = playerFactory;
            _hudController = hudController;
            _pauseManager = pauseManager;
            _characterMovementSystem = characterMovementSystem;
            _characterRotationSystem = characterRotationSystem;
            _cameraFollowSystem = cameraFollowSystem;
            _abilitySystem = abilitySystem;
            _weaponSystem = weaponSystem;
            _arenaController = arenaController;
            _arenaVictoryService = arenaVictoryService;
            _enemyMovementService = enemyMovementService;
            _enemyWeaponService = enemyWeaponService;
            _timerRunner = timerRunner;
            _projectileMovementService = projectileMovementService;
            _projectileHitService = projectileHitService;
            _despawnSystem = despawnSystem;
        }

        public void Start()
        {
            _playerFactory.Create();
            _hudController.Initialize();
        }

        public void Tick()
        {
            if (_pauseManager.IsPaused)
                return;

            var deltaTime = Time.deltaTime;

            _characterMovementSystem.Tick(deltaTime);
            _characterRotationSystem.Tick(deltaTime);
            _cameraFollowSystem.Tick(deltaTime);

            _abilitySystem.Tick(deltaTime);
            _weaponSystem.Tick(deltaTime);

            _timerRunner.Tick();

            _arenaController.Tick(deltaTime);
            _arenaVictoryService.Tick();
            _enemyMovementService.Tick(deltaTime);
            _enemyWeaponService.Tick(deltaTime);
            _projectileMovementService.Tick(deltaTime);
            _projectileHitService.Tick();

            _despawnSystem.Tick();
        }
    }
}
