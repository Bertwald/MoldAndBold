using MoldAndBold.Logic;
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
        { }
        internal static void ShowDaysOrderedByHumidity() 
        { }
        internal static void ShowDaysOrderedByMoldRisk()
        { }
        internal static void ShowSpecialDates()
        { }
        internal static void SearchByDate()
        { }
    }
}
