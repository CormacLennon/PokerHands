using System;
using System.Collections.Generic;
using System.Text;
using PokerHands.Model;

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

        public static string ToElementsString<T>(this IEnumerable<T> items)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in items)
            {
                stringBuilder.Append(item);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString().Trim();
        }
    }
}
