namespace MyBlockChain.General
{
    public class Hash
    {
        private readonly string _value = "";

        public static explicit operator string(Hash b)
        {
            return b._value;
        }
    }
}