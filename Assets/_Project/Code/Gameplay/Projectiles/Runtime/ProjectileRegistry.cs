using System.Collections.Generic;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Projectiles
{
    public sealed class ProjectileRegistry
    {
        private readonly List<ProjectileAgent> _items = new(64);
        private readonly Dictionary<InternalIntId, ProjectileAgent> _byId = new(64);

        public int Count => _items.Count;
        public IReadOnlyList<ProjectileAgent> Items => _items;

        public void Add(ProjectileAgent agent)
        {
            _byId[agent.Id] = agent;
            _items.Add(agent);
        }

        public bool TryGet(InternalIntId id, out ProjectileAgent agent) => _byId.TryGetValue(id, out agent);

        public void Remove(InternalIntId id)
        {
            if (!_byId.TryGetValue(id, out var agent))
                return;

            _byId.Remove(id);

            var index = _items.IndexOf(agent);
            if (index < 0)
                return;

            _items[index] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
        }
    }
}
