#region

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

#endregion

namespace MyBlockChain.General
{
    internal static class HashUtilities
    {
        public static string Hash(string rawData)
        {
            // Create a SHA256   
            using var sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            var builder = new StringBuilder();
            foreach (var t in bytes) builder.Append(t.ToString("x2"));
            return builder.ToString();
        }

        public static string Hash<T>(T data)
        {
            return SHA256.Create()
                         .ComputeHash(Encoding.UTF8
                                              .GetBytes(JsonSerializer.Serialize(data)))
                         .Aggregate(new StringBuilder(), (builder, value)
                                                             => builder.Append(value.ToString("x2")))
                         .ToString();
        }
    }
}