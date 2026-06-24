using System.Collections.Generic;

namespace _Project.Code.Gameplay.Timers
{
    public sealed class TimerRunner
    {
        private readonly List<Timer> _timers = new(64);

        public void Add(Timer timer) => _timers.Add(timer);

        public void Remove(Timer timer) => _timers.Remove(timer);

        public void Tick()
        {
            for (var i = _timers.Count - 1; i >= 0; i--)
                _timers[i].Tick();
        }
    }
}
