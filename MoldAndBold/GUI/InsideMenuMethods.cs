using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;
using static MoldAndBold.GUI.MenuMethods;
using MoldAndBold.Enums;

namespace MoldAndBold.GUI
{
    internal class InsideMenuMethods
    {
        internal static void ShowInsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowInsideDaysOrderedByTemp, ShowInsideDaysOrderedByHumidity, ShowInsideDaysOrderedByMoldRisk, MenuMethods.Return, MenuMethods.ExitProgram });
        }

        internal static void SearchByDate()
        {
            var date = Helper.GetDateFromUser();
            ShowByDate(date, Location.Inside);
        }

        internal static void ShowInsideDaysOrderedByTemp()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageTemperature, Location.Inside, "average temperature, coldest to hottest");
        }

        internal static void ShowInsideDaysOrderedByHumidity()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoisture, Location.Inside, "average humidity, highest to lowest");
        }

        internal static void ShowInsideDaysOrderedByMoldRisk()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoldRisk, Location.Inside, "average mold risk, highest to lowest");
        }
    }
}
