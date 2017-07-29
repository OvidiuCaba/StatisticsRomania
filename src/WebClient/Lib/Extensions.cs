using System;
using System.Collections.Generic;
using System.Linq;

namespace WebClient.Lib
{
    public static class Extensions
    {
        public static IOrderedEnumerable<TSource> OrderByWithDirection<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool ascending)
        {
            return ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }
    }
}