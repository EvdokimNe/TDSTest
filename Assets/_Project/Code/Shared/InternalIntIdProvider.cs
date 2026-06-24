namespace _Project.Code.Shared
{
    public sealed class InternalIntIdProvider
    {
        private int _nextId = 1;

        public InternalIntId Create()
        {
            return new InternalIntId(_nextId++);
        }
    }
}
