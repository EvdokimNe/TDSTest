namespace _Project.Code.Gameplay.Combat
{
    public readonly struct DamageRequest
    {
        public readonly DamagePayload Payload;
        public readonly ICombatEntity Target;

        public DamageRequest(DamagePayload payload, ICombatEntity target)
        {
            Payload = payload;
            Target = target;
        }
    }
}
