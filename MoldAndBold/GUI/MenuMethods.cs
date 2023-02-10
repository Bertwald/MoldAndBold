using MoldAndBold.Logic;
using MoldAndBold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.GUI
{
    internal static class MenuMethods
    {
        internal static void ShowInsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk });
        }
        internal static void ShowOutsideData() 
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, ShowSpecialDates });
        }
        internal static void ShowDaysOrderedByTemp()
        {
            ShowOrderedBy(x => x.AverageTemperature);
        }

        internal static void ShowDaysOrderedByHumidity() 
        { }
        internal static void ShowDaysOrderedByMoldRisk()
        { }
        internal static void ShowSpecialDates()
        { }
        internal static void SearchByDate()
        { }
        internal static void ShowOrderedBy(Func<AnnualData, double> ordning){
            // list<DailyData> data = loadAll();
            var ordered = DataLoader.LoadAllDays(Enums.Location.Inside).OrderBy(ordning);
            // Print
        }
    }
}
