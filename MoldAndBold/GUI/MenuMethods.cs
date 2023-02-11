using MoldAndBold.Logic;
using MoldAndBold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.GUI
{
    internal static class MenuMethods
    {
        internal static void ShowInsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, ExitProgram });
        }
        internal static void ShowOutsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, ShowSpecialDates, ExitProgram });
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
        {
            var specialDates = DataLoader.LoadAllDays(Enums.Location.Outside).Select(x => x);
            string dateInfo = "Special days of the year:" + Environment.NewLine + Environment.NewLine;
            foreach (var date in specialDates)
            {
                dateInfo += date.Year + Environment.NewLine;
                dateInfo += "First autumn day: " + date.AutumnArrival + Environment.NewLine;
                dateInfo += "First winter day: " + (date.WinterArrival == null ? "Didnt happen" : date.WinterArrival + Environment.NewLine) + Environment.NewLine + Environment.NewLine;
            }
            Console.WriteLine(dateInfo);
            Console.ReadKey();
        }
        internal static void SearchByDate()
        { }
        internal static void ShowOrderedBy(Func<AnnualData, double> ordning)
        {
            // list<DailyData> data = loadAll();
            var ordered = DataLoader.LoadAllDays(Enums.Location.Inside).OrderBy(ordning);
            // Print
        }
        internal static void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
