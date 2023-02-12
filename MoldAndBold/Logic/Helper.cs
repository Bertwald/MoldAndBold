using MoldAndBold.Models;
using static System.Environment;

namespace MoldAndBold.Logic
{
    internal class Helper
    {
        internal static DateOnly GetDateFromUser()
        {
            Console.Clear();
            Console.WriteLine("Enter date you would like to search for, format yyyy-MM-dd");
            var input = Console.ReadLine() ?? "";
            while (!DataLoader.ValidateDate(input))
            {
                Console.WriteLine("Wrong input, try again or enter \"exit\" to exit");
                input = Console.ReadLine() ?? "";
                if (input.ToLower() is "exit")
                    break;
            }
            return DateOnly.Parse(input);
        }

        internal static void PrintDailyDataFromDay(DailyData day)
        {
            Console.Clear();
            Console.WriteLine($"Inside/Outside: {day.Location + NewLine}" +
                $"Avarage temperature: {Math.Round(day.AverageTemperature, 2) + NewLine}" +
                $"Avarage humidity (%): {Math.Round(day.AverageMoisture, 2) + NewLine}" +
                $"Avarage mold risk: {Math.Round(day.AverageMoldRisk, 2) + NewLine + NewLine}" +
                $"Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void PrintOrderedBy(List<DailyData> ordered, string orderedBy, int number)
        {
            Console.Clear();
            var content = $"Top {number} days ordered by {orderedBy}" + NewLine;

            Console.WriteLine(content);
            //Print First Row
            Console.WriteLine("┌" + new string('─', 56) + "┐");
            Console.Write("│ ");
            PrintRainbow('♥', 54); 
            Console.WriteLine(" │");
            Console.WriteLine("│" + new string(' ', 5) + "Date" + new string(' ', 7) + "Temperature" + new string(' ', 5) + "Humidity" + new string(' ', 5) + "MoldIndex" + new string(' ', 2) + "│");
            var toPrint = ordered.Take(number);
            foreach(var dailyData in toPrint) {
                Console.WriteLine("│" + dailyData.Date.ToString().PadLeft(12) + Math.Round(dailyData.AverageTemperature, 2).ToString().PadLeft(12) + Math.Round(dailyData.AverageMoisture, 2).ToString().PadLeft(15) + Math.Round(dailyData.AverageMoldRisk, 2).ToString().PadLeft(10) + new string(' ', 7) + "│");
            }
            Console.WriteLine("└" + new string('─', 56) + "┘");
            /*
            for (int i = 0; i < number; i++)
            {
                content += $"[{i + 1}] Date: {ordered[i].Date} - AvgTemp: {Math.Round(ordered[i].AverageTemperature, 2) + NewLine}" +
                    $"\tAvgHumidity: {Math.Round(ordered[i].AverageMoisture , 2) + NewLine}" +
                    $"\tAvgMoldRisk: {Math.Round(ordered[i].AverageMoldRisk, 2)}" + NewLine;
            }
            */
            Console.WriteLine(NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        private static void PrintRainbow(char symbol, int number) {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.DarkYellow, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta };
            for(int n = 0; n< number; n++) {
                Console.ForegroundColor = colors[n % colors.Length];
                Console.Write(symbol);
            }
            Console.ResetColor();
        }

        internal static void ShowDailyData(List<DailyData> orderedData, string orderedBy)
        {
            Console.Clear();
            Console.WriteLine("How many days would you like to show?");
            var number = TryToParseInput();
            if (number <= orderedData.Count)
            {
                PrintOrderedBy(orderedData, orderedBy, number);
            }
            else
            {
                Console.WriteLine(number + " cannot be larger than the list, " + orderedData.Count);
                number = orderedData.Count;
                PrintOrderedBy(orderedData, orderedBy, number);
            }
        }

        public static int TryToParseInput()
        {
            var input = Console.ReadLine() ?? "";
            int number;
            while (!int.TryParse(input, out number))
            {
                Console.WriteLine("Wrong input, try again");
                input = Console.ReadLine() ?? "";
            }
            return number;
        }
    }
}
