namespace MyBlockChain.General
{
    public class Amount
    {
        private readonly int _value;

        private Amount(int value)
        {
            _value = value;
        }

        public static explicit operator int(Amount b)
        {
            return b._value;
        }

        public static explicit operator decimal(Amount b)
        {
            return b._value;
        }

        public static explicit operator Amount(decimal b)
        {
            return new(1);
        }

        public static bool operator <(Amount lhs, Amount rhs)
        {
            return lhs._value < rhs._value;
        }

        public static bool operator >(Amount lhs, Amount rhs)
        {
            return lhs._value > rhs._value;
        }

        public static Amount operator -(Amount lhs, Amount rhs)
        {
            return new(lhs._value - rhs._value);
        }

        public static Amount Create(int value)
        {
            return new(value);
        }
    }
}