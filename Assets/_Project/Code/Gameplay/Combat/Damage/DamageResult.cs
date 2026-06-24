namespace _Project.Code.Gameplay.Combat
{
    public readonly struct DamageResult
    {
        public readonly bool Applied;
        public readonly bool Killed;
        public readonly float Damage;

        public DamageResult(bool applied, bool killed, float damage)
        {
            Applied = applied;
            Killed = killed;
            Damage = damage;
        }
    }
}
