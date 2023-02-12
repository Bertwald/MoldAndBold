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
                content += $"[{i + 1}] Date: {orderedData[i].Date} - AvgTemp: {Math.Round(orderedData[i].AverageTemperature) + NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void PrintDailyDatasAvarageHumidity(List<DailyData> orderedData)
        {
            var content = "Top 10 days ordered by humidity, lowest to highest" + NewLine;
            for (int i = 0; i < 10; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - AvgHumidity: {Math.Round(orderedData[i].AverageMoisture) + NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void PrintDailyDatasAvarageMoldRisk(List<DailyData> orderedData)
        {
            var content = "Top 5 days ordered by mold risk, lowest to highest" + NewLine;
            for (int i = 0; i < 5; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - Risk: {Math.Round(orderedData[i].AverageMoisture)}% {NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void PrintList(List<DailyData> orderedData, string orderedBy, int number)
        {
            var content = $"Top {number} days ordered by {orderedBy}" + NewLine;
            for (int i = 0; i < number; i++)
            {
                content += $"[{i + 1}] Date: {orderedData[i].Date} - AvgTemp: {Math.Round(orderedData[i].AverageTemperature) + NewLine}" +
                    $"\tAvgHumidity: {Math.Round(orderedData[i].AverageMoisture) + NewLine}" +
                    $"\tAvgMoldRisk: {Math.Round(orderedData[i].AverageMoldRisk)} %" + NewLine;
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowDailyData(List<DailyData> orderedData, string orderedBy)
        {
            Console.WriteLine("How many days would you like to show?");
            var number = TryToParseInput();
            if (number <= orderedData.Count)
            {
                PrintList(orderedData, orderedBy, number);
            }
            else
            {
                Console.WriteLine(number + " cannot be larger than the list, " + orderedData.Count);
                number = orderedData.Count;
                PrintList(orderedData, orderedBy, number);
            }
        }

        public static int TryToParseInput()
        {
            var input = Console.ReadLine();
            int number;
            while (!int.TryParse(input, out number))
            {
                Console.WriteLine("Wrong input, try again");
                input = Console.ReadLine();
            }
            return number;
        }
    }
}
