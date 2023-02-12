using MoldAndBold.Enums;
using MoldAndBold.Logic;
using static MoldAndBold.GUI.MenuMethods;

namespace MoldAndBold.GUI {
    internal class InsideMenuMethods
    {
        internal static void DailyIndoorData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, MenuMethods.Return, MenuMethods.ExitProgram });
        }

        internal static void SearchByDate()
        {
            var date = Helper.GetDateFromUser();
            ShowByDate(date, Location.Inside);
        }

        internal static void ShowDaysOrderedByTemp()
        {
            MenuMethods.ShowOrderedBy(x => x.AverageTemperature, Location.Inside, "average temperature, coldest to hottest");
        }

        internal static void ShowDaysOrderedByHumidity()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoisture, Location.Inside, "average humidity, highest to lowest");
        }

        internal static void ShowDaysOrderedByMoldRisk()
        {
            MenuMethods.ShowOrderedBy(x => -x.AverageMoldRisk, Location.Inside, "average mold risk, highest to lowest");
        }
    }
}
