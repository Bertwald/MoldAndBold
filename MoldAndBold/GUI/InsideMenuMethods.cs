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
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDateInside, ShowInsideDaysOrderedByTemp, ShowInsideDaysOrderedByHumidity, ShowInsideDaysOrderedByMoldRisk, MenuMethods.Return, MenuMethods.ExitProgram });
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
            MenuMethods.ShowOrderedBy(x => x.AverageTemperature, Location.Inside, "avarage temperature, coldest to hottest");
        }

        internal static void ShowInsideDaysOrderedByHumidity()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageMoisture, Location.Inside, "avarage humidity, lowest to highest");
        }

        internal static void ShowInsideDaysOrderedByMoldRisk()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageMoldRisk, Location.Inside, "avarage mold risk, lowest to highest");
        }
    }
}
