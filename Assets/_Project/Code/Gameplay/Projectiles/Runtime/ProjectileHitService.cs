using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using _Project.Code.Gameplay.Despawn;
using _Project.Code.Gameplay.HitDetection;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileHitService
    {
        private readonly ProjectileRegistry _registry;
        private readonly HitQueryService _hitQueryService;
        private readonly DamageApplicationService _damageApplicationService;
        private readonly DespawnSystem _despawnSystem;

        private readonly List<HittableModule> _buffer = new(16);

        public ProjectileHitService(
            ProjectileRegistry registry,
            HitQueryService hitQueryService,
            DamageApplicationService damageApplicationService,
            DespawnSystem despawnSystem)
        {
            _registry = registry;
            _hitQueryService = hitQueryService;
            _damageApplicationService = damageApplicationService;
            _despawnSystem = despawnSystem;
        }

        public void Tick()
        {
            var items = _registry.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var agent = items[i];
                if (!agent.RuntimeData.IsAlive)
                    continue;

                var payload = agent.RuntimeData.DamagePayload;
                var excludeTeam = payload.Source != null ? payload.Source.Team : CombatTeam.None;
                var center = agent.View.transform.position;

                if (_hitQueryService.OverlapCircle(center, agent.RuntimeData.Radius, excludeTeam, _buffer) == 0)
                    continue;

                var target = _buffer[0].Entity;
                _damageApplicationService.ApplyDamage(payload.CreateRequest(target));
                _despawnSystem.RequestProjectile(agent.Id);
            }
        }
    }
}
