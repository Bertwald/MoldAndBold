using MoldAndBold.Enums;
using MoldAndBold.Logic;
using static MoldAndBold.GUI.MenuMethods;
using static System.Environment;

namespace MoldAndBold.GUI {
    internal class OutsideMenuMethods
    {
        internal static void ShowOutsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowOutsideDaysOrderedByTemp, ShowOutsideDaysOrderedByHumidity, ShowOutsideDaysOrderedByMoldRisk, ShowSpecialDates, MenuMethods.Return, MenuMethods.ExitProgram });
        }

        internal static void SearchByDate()
        {
            var date = Helper.GetDateFromUser();
            ShowByDate(date, Location.Outside);
            /*
            var day = DataLoader.GetDailyDataFromDate(date, Location.Outside);
            if (day is not null)
            {
                Helper.PrintDailyDataFromDay(day);
            }
            else
            {
                Console.WriteLine($"A day with date {date} could not be found. Press any key to continue");
                Console.ReadKey(true);
            }
            */
        }

        internal static void ShowOutsideDaysOrderedByTemp()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageTemperature, Location.Outside, "average temperature, coldest to hottest");
        }

        internal static void ShowOutsideDaysOrderedByHumidity()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoisture, Location.Outside, "average humidity, highest to lowest");
        }

        internal static void ShowOutsideDaysOrderedByMoldRisk()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoldRisk, Location.Outside, "average moldrisk, highest to lowest");
        }

        internal static void ShowSpecialDates()
        {
            Console.Clear();
            var specialDates = DataLoader.LoadAllDays(Location.Outside); //.Select(x => x);
            string dateInfo = "Special days of the year:" + NewLine + NewLine;
            foreach (var date in specialDates)
            {
                dateInfo += date.Year + NewLine;
                dateInfo += "First autumn day: " + date.AutumnArrival + NewLine;
                dateInfo += "First winter day: " + (date.WinterArrival == null ? "Didnt happen" : date.WinterArrival + NewLine) + NewLine + NewLine;
            }
            Console.WriteLine(dateInfo);
            Console.ReadKey();
        }
    }
}
