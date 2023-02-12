using MoldAndBold.Enums;
using MoldAndBold.Logic;
using MoldAndBold.Models;
using static System.Environment;

namespace MoldAndBold.GUI
{
    internal static class MenuMethods
    {
        internal static void ShowOrderedBy(Func<DailyData, double> ordering, Location location, string orderedBy)
        {
            var ordered = (DataLoader.LoadAllDays(location).ExtractDays()).OrderBy(ordering).ToList();
            Helper.ShowDailyData(ordered, orderedBy);
        }
        internal static void ShowByDate(DateOnly date, Location location) {
            var day = DataLoader.GetDailyDataFromDate(date, location);
            if (day is not null) {
                Helper.PrintDailyDataFromDay(day);
            } else {
                Console.WriteLine($"A day with date {date} could not be found. Press any key to continue");
                Console.ReadKey(true);
            }
        }

        internal static void ExitProgram()
        {
            Environment.Exit(0);
        }
        internal static void Return() {
            ; // Do Nothing
        }
        internal static void ShowSpecialDates() {
            Console.Clear();
            var specialDates = DataLoader.LoadAllDays(Location.Outside);
            string dateInfo = "Special days of the year:" + NewLine + NewLine;
            foreach (var date in specialDates) {
                dateInfo += date.Year + NewLine;
                dateInfo += "First autumn day: " + date.AutumnArrival + NewLine;
                dateInfo += "First winter day: " + (date.WinterArrival == null ? "Didnt happen" : date.WinterArrival + NewLine) + NewLine + NewLine;
            }
            Console.WriteLine(dateInfo);
            Console.ReadKey();
        }
    }
}
