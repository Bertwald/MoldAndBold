using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.GUI
{
    internal class OutsideMenuMethods
    {
        internal static void ShowOutsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { MenuMethods.SearchByDate, ShowOutsideDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, ShowSpecialDates, MenuMethods.ExitProgram });
        }

        internal static void ShowOutsideDaysOrderedByTemp()
        {
            //ShowOrderedBy(x => x.AverageTemperature);
            var lineBreak = Environment.NewLine;

            var allData = DataLoader.LoadAllDays(Enums.Location.Outside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageTemperature).ToList();

            var content = "Top 10 days ordered by temperatures, coldest to hottest" + lineBreak;
            for (int i = 0; i < 10; i++)
            {
                content += $"\tDate: {orderedData[i].Date} - AvgTemp: {orderedData[i].AverageTemperature + lineBreak}";
            }
            Console.WriteLine(content + "Press any key to continue");
            Console.ReadKey(true);
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
    }
}
