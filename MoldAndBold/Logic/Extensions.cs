using MoldAndBold.Models;
using System.Reflection;

namespace MoldAndBold.Logic {
    internal static class StringExtensions {

    }
    internal static class DelegateExtensions {
        public static string AsString(this Delegate action) {
            return string.Concat(action.GetMethodInfo().Name.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

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

    internal static class AnnualDataExtensions {
        public static List<DailyData> ExtractDays(this List<AnnualData> years) {
            return years.SelectMany(x => x.Months).SelectMany(x => x.Days).ToList();
        }
    }

}
