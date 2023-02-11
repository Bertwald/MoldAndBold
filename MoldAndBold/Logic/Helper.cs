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
    }
}
