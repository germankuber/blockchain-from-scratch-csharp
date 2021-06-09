using System;

namespace MyBlockChain.General
{
    public class Amount : IComparable<Amount>
    {
        private readonly int _value;

        private Amount(int value) => _value = value;

      

        public static implicit operator int(Amount b) =>
            b._value;

        public static explicit operator decimal(Amount b) =>
            b._value;

        public static implicit operator Amount(decimal b) =>
            new(Convert.ToInt32(b));

        public static bool operator <=(Amount lhs, Amount rhs) =>
            lhs._value <= rhs._value;

        public static bool operator >=(Amount lhs, Amount rhs) =>
            lhs._value >= rhs._value;

        public static Amount operator -(Amount lhs, Amount rhs) =>
            new(lhs._value - rhs._value);
        public static Amount operator +(Amount lhs, Amount rhs) =>
            new(lhs._value + rhs._value);

        public static Amount Create(int value) => new(value);

        public int CompareTo(Amount other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _value.CompareTo(other._value);
        }
    }
}