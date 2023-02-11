using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;
using MoldAndBold.Enums;

namespace MoldAndBold.GUI
{
    internal class InsideMenuMethods
    {
        internal static void ShowInsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDateInside, ShowInsideDaysOrderedByTemp, ShowInsideDaysOrderedByHumidity, ShowInsideDaysOrderedByMoldRisk, MenuMethods.ExitProgram });
        }

        internal static void SearchByDateInside()
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

        internal static void ShowInsideDaysOrderedByTemp()
        {
            var allData = DataLoader.LoadAllDays(Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageTemperature).ToList();
            Helper.PrintDailyDatasAvarageTemp(orderedData);
        }

        internal static void ShowInsideDaysOrderedByHumidity()
        {
            var allData = DataLoader.LoadAllDays(Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoisture).ToList();
            Helper.PrintDailyDatasAvarageHumidity(orderedData);
        }

        internal static void ShowInsideDaysOrderedByMoldRisk()
        {
            var allData = DataLoader.LoadAllDays(Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoldRisk).ToList();
            Helper.PrintDailyDatasAvarageMoldRisk(orderedData);
        }
    }
}
