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
            ActionSelector.ExecuteActionFromList(new List<Action> { MenuMethods.SearchByDate, ShowInsideDaysOrderedByTemp, ShowInsideDaysOrderedByHumidity, ShowInsideDaysOrderedByMoldRisk, MenuMethods.ExitProgram });
        }

        internal static void ShowInsideDaysOrderedByTemp()
        {
            var lineBreak = Environment.NewLine;
            var allData = DataLoader.LoadAllDays(Enums.Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageTemperature).ToList();
            var content = "Top 10 days ordered by temperatures, coldest to hottest" + lineBreak;
            for (int i = 0; i < 10; i++)
            {
                content += $"{i + 1}\tDate: {orderedData[i].Date} - AvgTemp: {orderedData[i].AverageTemperature + lineBreak}";
            }
            Console.WriteLine(content + lineBreak + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowInsideDaysOrderedByHumidity()
        {
            var lineBreak = Environment.NewLine;
            var allData = DataLoader.LoadAllDays(Enums.Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoisture).ToList();
            var content = "Top 10 days ordered by humidity, lowest to highest" + lineBreak;
            for (int i = 0; i < 10; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - AvgHumidity: {orderedData[i].AverageMoisture + lineBreak}";
            }
            Console.WriteLine(content + lineBreak + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowInsideDaysOrderedByMoldRisk()
        {
        
        }
    }
}
