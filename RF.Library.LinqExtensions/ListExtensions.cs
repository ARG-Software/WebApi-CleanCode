namespace RF.Library.LinqExtensions
{
    using System.Linq;
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static IEnumerable<T> ReturnNullIfEmpty<T>(this IEnumerable<T> source)
        {
            var returnNullIfEmpty = source.ToList();
            return returnNullIfEmpty.Any() ? returnNullIfEmpty : null;
        }
    }
}