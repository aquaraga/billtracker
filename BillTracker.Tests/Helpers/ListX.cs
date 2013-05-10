using System;
using System.Collections.Generic;
using System.Linq;

namespace BillTracker.Tests.Helpers
{
    public static class ListX
    {
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return source.All(other.Contains);
        }

        public static bool MatchesAll<T>(this IEnumerable<T> source, Predicate<T> condition)
        {
            return source.All(s => condition(s));
        }
    }
}
