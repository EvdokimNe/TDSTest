using System;
namespace _Project.Code.Shared
{
    public readonly struct InternalIntId : IEquatable<InternalIntId>
    {
        public int Value { get; }

        public InternalIntId(int value)
        {
            Value = value;
        }

        public bool Equals(InternalIntId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is InternalIntId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
