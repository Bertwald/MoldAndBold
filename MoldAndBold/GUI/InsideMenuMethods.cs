using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.GUI
{
    internal class InsideMenuMethods
    {
        internal static void ShowInsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { MenuMethods.SearchByDate, ShowInsideDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk, MenuMethods.ExitProgram });
        }

        internal static void ShowInsideDaysOrderedByTemp()
        {
            //ShowOrderedBy(x => x.AverageTemperature);
            var lineBreak = Environment.NewLine;

            var allData = DataLoader.LoadAllDays(Enums.Location.Inside);
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
    }
}
