#region

using System;
using CSharpFunctionalExtensions;

#endregion

namespace MyBlockChain.General
{
    public class Amount : ValueObject<Amount>, IComparable<Amount>
    {
        private readonly int _value;

        private Amount(int value)
        {
            _value = value;
        }

        public int CompareTo(Amount other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _value.CompareTo(other._value);
        }


        public static implicit operator int(Amount b)
        {
            return b._value;
        }

        public static explicit operator decimal(Amount b)
        {
            return b._value;
        }

        public static implicit operator string(Amount b)
        {
            return b._value.ToString();
        }

        public static implicit operator Amount(decimal b)
        {
            return new(Convert.ToInt32(b));
        }

        public static bool operator <=(Amount lhs, Amount rhs)
        {
            return lhs._value <= rhs._value;
        }

        public static bool operator >=(Amount lhs, Amount rhs)
        {
            return lhs._value >= rhs._value;
        }

        public static Amount operator -(Amount lhs, Amount rhs)
        {
            return new(lhs._value - rhs._value);
        }

        public static Amount operator +(Amount lhs, Amount rhs)
        {
            return new(lhs._value + rhs._value);
        }

        public static Amount Create(int value)
        {
            return new(value);
        }

        protected override bool EqualsCore(Amount other)
        {
            return _value == other._value;
        }

        protected override int GetHashCodeCore()
        {
            return _value.GetHashCode();
        }
    }
}