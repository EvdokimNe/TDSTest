using R3;

namespace _Project.Code.Gameplay.Combat
{
    public sealed class HealthState
    {
        public readonly ReactiveProperty<float> Current;
        public float Max;

        public bool IsAlive => Current.Value > 0f;

        public HealthState(float max)
        {
            Max = max;
            Current = new ReactiveProperty<float>(max);
        }
    }
}
