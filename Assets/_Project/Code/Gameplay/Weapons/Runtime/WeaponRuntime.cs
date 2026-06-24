namespace _Project.Code.Gameplay.Weapons
{
    public sealed class WeaponRuntime
    {
        public readonly WeaponConfig Config;
        public float CooldownRemaining;

        public bool IsReady => CooldownRemaining <= 0f;

        public WeaponRuntime(WeaponConfig config)
        {
            Config = config;
        }
    }
}
