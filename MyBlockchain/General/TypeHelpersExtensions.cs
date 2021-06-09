using System;

namespace MyBlockChain.General
{
    public static class TypeHelpersExtensions
    {
        public static string TransformToString(this byte[] @this)
        {
            return Convert.ToHexString(@this);
        }

        public static byte[] ToByte(this string @this)
        {
            return Convert.FromHexString(@this);
        }

        public static byte[] ToByte(this Address @this)
        {
            return Convert.FromHexString(@this);
        }
    }
}