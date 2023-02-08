using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.Logic {
    internal static class StringExtensions {

    }

    public static class ListExtensions {
        public static (List<T>, List<T>) Split<T>(this List<T> toSplit, Predicate<T> predicate) {
            List<T> first = toSplit.Where(x => predicate(x)).ToList();
            List<T> second = toSplit.Where(x => !predicate(x)).ToList();

            return (first, second);
        }
    }
    // SHOW: Fancy .Net 7 good stuff
    public static class IEnumerableExtensions {
        //public static double GetMeanValue<T>(this IEnumerable<T> values) where T : INumber<T>, IConvertible {
        //    T result = T.Zero;
        //    foreach (T value in values) {
        //        result += value;
        //    }
        //    return result.ToDouble(null) / values.Count();
        //}

        //public static T GetMeanTypeValue<T>(this IEnumerable<T> values) where T : INumber<T>, IParsable<T> {
        //    T result = T.Zero;
        //    foreach (T value in values) {
        //        result += value;
        //    }
        //    return result / T.Parse(values.Count().ToString(), null);
        //}
    }

}
