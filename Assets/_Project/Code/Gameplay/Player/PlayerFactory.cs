using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.HitDetection;
using _Project.Code.Gameplay.PlayerMovement;
using _Project.Code.Gameplay.Weapons;
using _Project.Code.Infrastructure.Configs;
using _Project.Code.Shared;
using UnityEngine;

namespace _Project.Code.Gameplay.Player
{
    public sealed class PlayerFactory
    {
        private const float MoveSpeed = 10f;

        private readonly PlayerProvider _provider;
        private readonly PlayerView _playerView;
        private readonly PlayerWeaponView _weaponViewPrefab;
        private readonly InternalIntIdProvider _idProvider;
        private readonly CombatEntityRegistry _combatEntityRegistry;
        private readonly HittableRegistry _hittableRegistry;
        private readonly ConfigService _configService;

        public PlayerFactory(
            PlayerProvider provider,
            PlayerView playerView,
            PlayerWeaponView weaponViewPrefab,
            InternalIntIdProvider idProvider,
            CombatEntityRegistry combatEntityRegistry,
            HittableRegistry hittableRegistry,
            ConfigService configService)
        {
            _provider = provider;
            _playerView = playerView;
            _weaponViewPrefab = weaponViewPrefab;
            _idProvider = idProvider;
            _combatEntityRegistry = combatEntityRegistry;
            _hittableRegistry = hittableRegistry;
            _configService = configService;
        }

        public void Create()
        {
            _configService.TryGet<PlayerConfig>(out var config);

            var combat = new PlayerCombatEntity(_idProvider.Create());
            var state = _combatEntityRegistry.Register(
                combat,
                config.StatsConfig,
                config.StatsConfig.GetOrDefault(StatType.MaxHealth),
                config.DefenseModifiers);

            var hittable = new HittableModule(combat, _playerView.transform, _playerView.Radius);
            _hittableRegistry.Add(hittable);

            var weaponView = Object.Instantiate(_weaponViewPrefab, _playerView.transform, false);

            _provider.PlayerView = _playerView;
            _provider.Motor = new MovementMotor(_playerView.CharacterController, MoveSpeed);
            _provider.Combat = combat;
            _provider.CombatEntityState = state;
            _provider.Hittable = hittable;
            _provider.WeaponView = weaponView;
        }
    }
}
