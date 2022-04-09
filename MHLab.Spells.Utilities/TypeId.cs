using System;

namespace MHLab.Spells.Utilities
{
    public readonly struct TypeId : IEquatable<TypeId>
    {
        private readonly int _id;

        internal TypeId(int id)
        {
            _id = id;
        }
        
        public bool Equals(TypeId other)
        {
            return _id == other._id;
        }

        public override bool Equals(object obj)
        {
            return obj is TypeId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public static bool operator ==(TypeId left, TypeId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TypeId left, TypeId right)
        {
            return !left.Equals(right);
        }
    }
    
    public static class TypeMapper<TType>
    {
        public static readonly TypeId Id = TypeIdGenerator.Get();
    }

    internal static class TypeIdGenerator
    {
        private static int _counter;

        public static TypeId Get()
        {
            var counter = _counter;
            _counter++;
            return new TypeId(counter);
        }
    }
}