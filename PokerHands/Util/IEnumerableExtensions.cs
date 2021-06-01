using System;
using System.Collections.Generic;

namespace PokerHands.Util
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctOn<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
