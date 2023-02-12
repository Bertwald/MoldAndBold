using MoldAndBold.Enums;
using MoldAndBold.Logic;
using static MoldAndBold.GUI.MenuMethods;

namespace MoldAndBold.GUI {
    internal class OutsideMenuMethods
    {
        internal static void DailyOutdoorData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, MenuMethods.Return, MenuMethods.ExitProgram });
        }

        internal static void SearchByDate()
        {
            var date = Helper.GetDateFromUser();
            ShowByDate(date, Location.Outside);
        }

        internal static void ShowDaysOrderedByTemp()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageTemperature, Location.Outside, "average temperature, coldest to hottest");
        }

        internal static void ShowDaysOrderedByHumidity()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoisture, Location.Outside, "average humidity, highest to lowest");
        }

        internal static void ShowDaysOrderedByMoldRisk()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoldRisk, Location.Outside, "average moldrisk, highest to lowest");
        }
    }
}
