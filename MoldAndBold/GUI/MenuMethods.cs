using MoldAndBold.Enums;
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
        internal static void ShowDailyDataFromDay(DailyData day)
        {
            var lineBreak = Environment.NewLine;
            Console.WriteLine($"Inside/Outside: {day.Location + lineBreak}" +
                $"Avarage temperature: {Math.Round(day.AverageTemperature) + lineBreak}" +
                $"Avarage humidity (%): {Math.Round(day.AverageMoisture) + lineBreak}" +
                $"Avarage mold risk: {Math.Round(day.AverageMoldRisk) + lineBreak + lineBreak}" +
                $"Press any key to continue");
            Console.ReadKey(true);
        }

        internal static DailyData? GetDayFromDate(DateOnly date)
        {
            return DataLoader.LoadAllDays(Location.Inside)
                .SelectMany(x => x.Months.SelectMany(x => x.Days))
                .Where(x => x.Date == date)
                .FirstOrDefault();
        }

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
