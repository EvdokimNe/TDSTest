using System.Collections.Generic;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.Enemies
{
    public sealed class EnemyRegistry
    {
        private readonly List<EnemyAgent> _items = new(32);
        private readonly Dictionary<InternalIntId, EnemyAgent> _byId = new(32);

        public int Count => _items.Count;
        public IReadOnlyList<EnemyAgent> Items => _items;

        public void Add(EnemyAgent agent)
        {
            _byId[agent.Id] = agent;
            _items.Add(agent);
        }

        public bool TryGet(InternalIntId id, out EnemyAgent agent) => _byId.TryGetValue(id, out agent);

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
