using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class DamageApplicationService
    {
        private readonly CombatEntityRegistry _registry;
        private readonly DamagePipeline _damagePipeline;
        private readonly CombatDeathStream _deathStream;

        public DamageApplicationService(
            CombatEntityRegistry registry,
            DamagePipeline damagePipeline,
            CombatDeathStream deathStream)
        {
            _registry = registry;
            _damagePipeline = damagePipeline;
            _deathStream = deathStream;
        }

        public DamageResult ApplyDamage(DamageRequest request)
        {
            if (!_registry.TryGet(request.Target, out var targetState))
                return new DamageResult(false, false, 0f);

            if (!TryValidate(request, targetState))
                return new DamageResult(false, false, 0f);

            var calc = _damagePipeline.Calculate(request, targetState);

            targetState.Health.Current.Value = Mathf.Max(0f, targetState.Health.Current.Value - calc.FinalDamage);

            var killed = targetState.Health.Current.Value <= 0f;
            if (killed)
                _deathStream.Publish(request.Target);

            return new DamageResult(true, killed, calc.FinalDamage);
        }

        private bool TryValidate(DamageRequest request, CombatEntityState targetState)
        {
            if (request.Payload.Source == null || request.Target == null)
                return false;

            if (request.Payload.Source.Team == request.Target.Team && request.Payload.Source.Team != CombatTeam.None)
                return false;

            if (targetState.Health == null || !targetState.Health.IsAlive)
                return false;

            return true;
        }
    }
}
