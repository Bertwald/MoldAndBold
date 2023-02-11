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
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDateOutside, ShowOutsideDaysOrderedByTemp, ShowOutsideDaysOrderedByHumidity, ShowOutsideDaysOrderedByMoldRisk, ShowSpecialDates, MenuMethods.ExitProgram });
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
            var allData = DataLoader.LoadAllDays(Location.Outside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageTemperature).ToList();
            Helper.PrintDailyDatasAvarageTemp(orderedData);
        }

        internal static void ShowOutsideDaysOrderedByHumidity()
        {
            var allData = DataLoader.LoadAllDays(Location.Outside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoisture).ToList();
            Helper.PrintDailyDatasAvarageHumidity(orderedData);
        }

        internal static void ShowOutsideDaysOrderedByMoldRisk()
        {
            var allData = DataLoader.LoadAllDays(Location.Outside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoldRisk).ToList();
            Helper.PrintDailyDatasAvarageMoldRisk(orderedData);
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
