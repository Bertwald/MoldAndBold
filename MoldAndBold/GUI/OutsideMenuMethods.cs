using MoldAndBold.Enums;
using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace MoldAndBold.GUI
{
    internal class OutsideMenuMethods
    {
        internal static void ShowOutsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDateOutside, ShowOutsideDaysOrderedByTemp, ShowOutsideDaysOrderedByHumidity, ShowOutsideDaysOrderedByMoldRisk, ShowSpecialDates, MenuMethods.Return, MenuMethods.ExitProgram });
        }

        internal static void SearchByDateOutside()
        {
            var date = Helper.GetDateFromUser();
            var day = DataLoader.GetDailyDataFromDate(date);
            if (day is not null)
            {
                Helper.PrintDailyDataFromDay(day);
            }
            else
            {
                Console.WriteLine($"A day with date {date} could not be found. Press any key to continue");
                Console.ReadKey(true);
            }
        }

        internal static void ShowOutsideDaysOrderedByTemp()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageTemperature, Location.Outside, "avarage temperature, coldest to hottest");
        }

        internal static void ShowOutsideDaysOrderedByHumidity()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageMoisture, Location.Outside, "avarage humidity, lowest to highest");
        }

        internal static void ShowOutsideDaysOrderedByMoldRisk()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageMoldRisk, Location.Outside, "avarage moldrisk, lowest to highest");
        }

        internal static void ShowSpecialDates()
        {
            var specialDates = DataLoader.LoadAllDays(Location.Outside).Select(x => x);
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
