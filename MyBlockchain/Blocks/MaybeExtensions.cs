using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;

namespace MyBlockChain.Blocks
{
    public static class MaybeExtensions
    {
        public static Maybe<List<T>> ToMaybe<T>(this List<T> @this)
        {
            return @this.Any()
                ? Maybe<List<T>>.From(@this)
                : Maybe<List<T>>.None;
        }
    }
}