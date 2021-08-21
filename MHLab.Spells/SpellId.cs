using System;

namespace MHLab.Spells
{
    public readonly struct SpellId : IEquatable<SpellId>
    {
        private readonly uint _id;

        public SpellId(uint id)
        {
            _id = id;
        }

        public bool Equals(SpellId other)
        {
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return obj is SpellId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (int)_id;
        }

        public static bool operator ==(SpellId left, SpellId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SpellId left, SpellId right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}