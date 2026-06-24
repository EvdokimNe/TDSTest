namespace _Project.Code.Gameplay.Combat
{
    public sealed class DamageContext
    {
        public DamageRequest Request;
        public float CurrentDamage;

        public void Reset(DamageRequest request)
        {
            Request = request;
            CurrentDamage = request.Payload.Damage;
        }
    }
}
