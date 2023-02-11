using MoldAndBold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace MoldAndBold.Logic
{
    internal class Helper
    {
        internal static DateOnly GetDateFromUser()
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
            return DateOnly.Parse(input);
        }

        internal static void PrintDailyDataFromDay(DailyData day)
        {
            Console.WriteLine($"Inside/Outside: {day.Location + NewLine}" +
                $"Avarage temperature: {Math.Round(day.AverageTemperature) + NewLine}" +
                $"Avarage humidity (%): {Math.Round(day.AverageMoisture) + NewLine}" +
                $"Avarage mold risk: {Math.Round(day.AverageMoldRisk) + NewLine + NewLine}" +
                $"Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void PrintDailyDatasAvarageTemp(List<DailyData> orderedData)
        {
            var content = "Top 10 days ordered by temperatures, coldest to hottest" + NewLine;
            for (int i = 0; i < 10; i++)
            {
                content += $"{i + 1}\tDate: {orderedData[i].Date} - AvgTemp: {Math.Round(orderedData[i].AverageTemperature) + NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }
    }
}
