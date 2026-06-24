using System.Collections.Generic;
using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.HitDetection
{
    public sealed class HitQueryService
    {
        private readonly HittableRegistry _registry;

        public HitQueryService(HittableRegistry registry)
        {
            _registry = registry;
        }
        
        public int OverlapCircle(Vector3 center, float radius, CombatTeam excludeTeam, List<HittableModule> results)
        {
            results.Clear();

            var items = _registry.Items;
            for (var i = 0; i < items.Count; i++)
            {
                var hittable = items[i];

                if (excludeTeam != CombatTeam.None && hittable.Entity.Team == excludeTeam)
                    continue;

                var position = hittable.Position;
                var dx = position.x - center.x;
                var dz = position.z - center.z;
                var reach = radius + hittable.Radius;

                if (dx * dx + dz * dz <= reach * reach)
                    results.Add(hittable);
            }

            return results.Count;
        }
    }
}
