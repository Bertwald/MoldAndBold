﻿using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;
using MoldAndBold.Enums;

namespace MoldAndBold.GUI
{
    internal class InsideMenuMethods
    {
        internal static void ShowInsideData()
        {
            ActionSelector.ExecuteActionFromList(new List<Action> { SearchByDateInside, ShowInsideDaysOrderedByTemp, ShowInsideDaysOrderedByHumidity, ShowInsideDaysOrderedByMoldRisk, MenuMethods.Return, MenuMethods.ExitProgram });
        }

        internal static void SearchByDateInside()
        {
            var date = Helper.GetDateFromUser();
            var day = DataLoader.GetDailyDataFromDate(date);
            if (day is not null)
            {
                Helper.PrintDailyDataFromDay(day);
            }
            else
            {
                Console.WriteLine($"A day with date {date} could not be found. Press any key to continue");
                Console.ReadKey(true);
            }
        }

        internal static void ShowInsideDaysOrderedByTemp()
        {
            var allData = DataLoader.LoadAllDays(Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => x.AverageTemperature).ToList();
            var content = "Top 10 days ordered by temperatures, coldest to hottest" + NewLine;
            for (int i = 0; i < 10; i++)
            {
                content += $"{i + 1}\tDate: {orderedData[i].Date} - AvgTemp: {Math.Round(orderedData[i].AverageTemperature, 2) + NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowInsideDaysOrderedByHumidity()
        {
            var allData = DataLoader.LoadAllDays(Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => -x.AverageMoisture).ToList();
            var content = "Top 10 days ordered by humidity, lowest to highest" + NewLine;
            for (int i = 0; i < 10; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - AvgHumidity: {Math.Round(orderedData[i].AverageMoisture, 2) + NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }

        internal static void ShowInsideDaysOrderedByMoldRisk()
        {
            var allData = DataLoader.LoadAllDays(Location.Inside);
            var orderedData = allData.SelectMany(x => x.Months.SelectMany(x => x.Days)).OrderBy(x => -x.AverageMoldRisk).ToList();
            var content = "Top 5 days ordered by mold risk, lowest to highest" + NewLine;
            for (int i = 0; i < 5; i++)
            {
                content += $"Number: {i + 1}\tDate: {orderedData[i].Date} - Risk: {Math.Round(orderedData[i].AverageMoldRisk, 2)}% {NewLine}";
            }
            Console.WriteLine(content + NewLine + "Press any key to continue");
            Console.ReadKey(true);
        }
    }
}
