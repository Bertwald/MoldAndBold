using MoldAndBold.Enums;
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
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDateOutside, ShowOutsideDaysOrderedByTemp, ShowOutsideDaysOrderedByHumidity, ShowOutsideDaysOrderedByMoldRisk, ShowSpecialDates, MenuMethods.ExitProgram });
        }

        internal static void SearchByDateOutside()
        {
            Console.WriteLine("Enter date you would like to search for, format yyyy-MM-dd");
            var input = Console.ReadLine();
            while (!DataLoader.ValidateDate(input))
            {
                Console.WriteLine("Wrong input, try again or enter \"exit\" to exit");
                input = Console.ReadLine();
                if (input.ToLower() is "exit")
                    break;
            }
            var date = DateOnly.Parse(input);
            var day = DataLoader.LoadAllDays(Location.Outside)
                .SelectMany(x => x.Months.SelectMany(x => x.Days))
                .Where(x => x.Date == date)
                .FirstOrDefault();
            if (day is not null)
            {
                var lineBreak = Environment.NewLine;
                Console.WriteLine($"Inside/Outside: {day.Location + lineBreak}" +
                    $"Avarage temperature: {Math.Round(day.AverageTemperature) + lineBreak}" +
                    $"Avarage humidity (%): {Math.Round(day.AverageMoisture) + lineBreak}" +
                    $"Avarage mold risk: {Math.Round(day.AverageMoldRisk) + lineBreak + lineBreak}" +
                    $"Press any key to continue");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine($"A day with date {date} could not be found. Press any key to continue");
                Console.ReadKey(true);
            }
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
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - AvgTemp: {Math.Round(orderedData[i].AverageTemperature) + lineBreak}";
            }
            Console.WriteLine(content + lineBreak + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowOutsideDaysOrderedByHumidity()
        {
            var lineBreak = Environment.NewLine;
            var allData = DataLoader.LoadAllDays(Enums.Location.Outside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoisture).ToList();
            var content = "Top 10 days ordered by humidity, lowest to highest" + lineBreak;
            for (int i = 0; i < 10; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - AvgHumidity: {Math.Round(orderedData[i].AverageMoisture) + lineBreak}";
            }
            Console.WriteLine(content + lineBreak + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowOutsideDaysOrderedByMoldRisk()
        {
            var lineBreak = Environment.NewLine;
            var allData = DataLoader.LoadAllDays(Enums.Location.Outside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageMoldRisk).ToList();
            var content = "Top 5 days ordered by mold risk, lowest to highest" + lineBreak;
            for (int i = 0; i < 5; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - Risk: {Math.Round(orderedData[i].AverageMoisture)}% {lineBreak}";
            }
            Console.WriteLine(content + lineBreak + "Press any key to continue");
            Console.ReadKey(true);
        }

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
