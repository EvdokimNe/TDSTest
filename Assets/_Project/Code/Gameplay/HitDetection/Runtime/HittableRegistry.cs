using System.Collections.Generic;
using _Project.Code.Shared;

namespace _Project.Code.Gameplay.HitDetection
{
    public sealed class HittableRegistry
    {
        private readonly List<HittableModule> _items = new(64);
        private readonly Dictionary<InternalIntId, HittableModule> _byId = new(64);

        public int Count => _items.Count;
        public IReadOnlyList<HittableModule> Items => _items;

        public void Add(HittableModule hittable)
        {
            _byId[hittable.Entity.Id] = hittable;
            _items.Add(hittable);
        }

        public void Remove(InternalIntId id)
        {
            if (!_byId.TryGetValue(id, out var hittable))
                return;

            _byId.Remove(id);

            var index = _items.IndexOf(hittable);
            if (index < 0)
                return;

            _items[index] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);
        }
    }
}
