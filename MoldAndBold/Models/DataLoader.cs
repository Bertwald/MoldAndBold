using MoldAndBold.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;

namespace MoldAndBold.Models {
    internal class DataLoader {

        internal static List<DataPoint> GetDailyData() {

            // Check if data exists in aggregated file
            // If No
            // Check if "tempdata5-med fel.txt" is present and can be read
            // Yes, Generate and save aggregate data
            // No, Fail Panic and blame someone else
            // If Yes
            // Load contents and return

            return new List<DataPoint>();
        }

        internal static void ConstructData() {
            string rows = LoadTempData();
            //string regexString11 = """(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<location>Inne|Ute),(?<temp>-?\d{1,2}.\d{1,2}),(?<moisture>\d{1,2})""";
            string regexString = "(?<date>\\d{4}-\\d{2}-\\d{2}) (?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>-?\\d{1,2}.\\d{1,2}),(?<moisture>\\d{1,2})";


            //var dailyRawData = rows.Split(Environment.NewLine).GroupBy(x => x[..10]).ToList();
            //var MonthyData = dailyRawData.GroupBy(x => x.Key[5..7]).Where(x => int.Parse(x.Key) > 5 && int.Parse(x.Key) < 13);

            //foreach (var month in MonthyData) {


            //    Console.WriteLine($"{month.Key}: Has values");
            //    Console.WriteLine(month.ToList().Count);
            //    if (IsMonthlyDatacomplete(int.Parse(month.Key), month.ToList().Count)) {
            //        Console.WriteLine($"{month.Key}: Has complete Data");
            //    } else {
            //        Console.WriteLine($"{month.Key}: Has incomplete Data");
            //    }
            //    List<DailyData> dailyIndoorData = new();
            //    List<DailyData> dailyOutdoorData = new();
            //    //var locationData = month.GroupBy(x => x.Key[20..])

            //    //foreach (var day in month) {
            //    //    var matches = regex.Matches(string.Join(' ', day.ToList()));
            //    //    var locationMatches = matches.GroupBy(x => x.Groups["location"]);
            //    //    IEnumerable<string> dailyTemps, dailyMoists;
            //    //        dailyTemps = locationMatches.Select(x => x.Groups["temp"].Value);
            //    //        dailyMoists = locationMatches.Select(x => x.Groups["moisture"].Value);
            //    //    //dailyIndoorData.Add(new DailyData { Date = DateOnly.Parse(day.Key[..10]), AverageTemperature = GetAverageTemperature(), AverageMoisture = GetAverageMoisture(day), AverageMoldRisk = GetAverageMoldRisk(day) });
            //    //}

            //}

            Console.WriteLine($"Double avg: {(new List<double> { 1.0D, 1.0D, 8.0D }).GetMeanTypeValue()}");
            Console.WriteLine($"Integer avg: {(new List<int> { 1, 1, 8 }).GetMeanTypeValue()}");


            List<DataPoint> dataset = ConstructDataset(rows);
            Console.WriteLine($"Number of datapoints: {dataset.Count}");

            // TODO: Purge Datapoints from invalid temperature and moisture data

            (List<DataPoint> inside, List<DataPoint> outside) = dataset.Split(x => x.Location == Location.Inne);
            Console.WriteLine($"Number of datapoints inside: {inside.Count}");
            Console.WriteLine($"Number of datapoints outside: {outside.Count}");

            Console.WriteLine($"Inside Maxii : {inside.Select(x => x.Temperature).Max()}    {inside.Select(x => x.Moisture).Max()}");
            Console.WriteLine($"Outside Maxii : {outside.Select(x => x.Temperature).Max()}    {outside.Select(x => x.Moisture).Max()}");

            Console.WriteLine($"Inside Minimii : {inside.Select(x => x.Temperature).Min()}    {inside.Select(x => x.Moisture).Min()}");
            Console.WriteLine($"Outside Minimii: {outside.Select(x => x.Temperature).Min()}    {outside.Select(x => x.Moisture).Min()}");

            Console.WriteLine($"Inside avg: {inside.Select(x => x.Temperature).Average()}");
            Console.WriteLine($"Outside avg: {outside.Select(x => x.Temperature).Average()}");
            //TODO: Handle inside and outside Datapoints in own methods
            ConstructInsideSeries(inside);
            ConstructOutsideSeries(outside);
            //TODO: 







            //var groupedMatches = regex.Matches(rows)
            //                    .GroupBy(x => new { location = x.Groups["location"] });
            //                    //.Select(x => new { x.Key.location, vals = x.ToList() });

            //foreach (var location in groupedMatches) {
            //    Console.WriteLine("Should only print twice");
            //    var monthlyMatches = location
            //        .GroupBy(x => x.Groups["date"].Value[5..7]);
            //        //.Select(x => new { month = x.Key, vals = x.ToList() });

            //    foreach (var monthly in monthlyMatches) {
            //        var dailyMatches = monthly
            //                           .GroupBy(x => x.Groups["date"]);
            //                           //.Select(x => new { day = x.Key, vals = x.ToList() });
            //        foreach (var day in dailyMatches) {
            //            IEnumerable<double> temps = day.Select(x => double.Parse(x.Groups["temp"].Value, CultureInfo.InvariantCulture)).ToList();
            //        }
            //    }
            //}



            //if (ValidateDate(grouped.date.Value)) {
            //    Console.WriteLine($"{grouped.date} {grouped.location} {grouped.vals.Count}");
            //    foreach (Group group in match.Groups) {
            //        Console.WriteLine(group.Name + " " + group.Value);
            //    }

            //    Console.WriteLine(match.Name);
            //}
        }

        private static void ConstructOutsideSeries(List<DataPoint> outside) {
            throw new NotImplementedException();
        }

        private static void ConstructInsideSeries(List<DataPoint> inside) {
            throw new NotImplementedException();
        }

        private static string LoadTempData() {
            try {
                string rows;
                using (StreamReader reader = new("../../../" + "LocalOnly/tempdata5-med fel.txt")) {
                    rows = reader.ReadToEnd();
                }
                return rows;
            }
            catch (Exception e) {
                Console.WriteLine("A File could not be used:");
                Console.WriteLine(e.Message);
            }
            return String.Empty;
        }

        private static List<DataPoint> ConstructDataset(string rows) {
            Regex regex = new("(?<date>\\d{4}-\\d{2}-\\d{2}) (?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>-?\\d{1,2}.\\d{1,2}),(?<moisture>\\d{1,2})");
            List<DataPoint> dataset = new();
            foreach (Match match in regex.Matches(rows).Where(x => ValidateDate(x.Groups["date"].Value) && ValidateTime(x.Groups["time"].Value))) {
                dataset.Add(new DataPoint() {
                    Date = DateTime.Parse(match.Groups["date"].Value + " " + match.Groups["time"].Value),
                    Location = (Location)Enum.Parse(typeof(Location), match.Groups["location"].Value),
                    Moisture = int.Parse(match.Groups["moisture"].Value),
                    Temperature = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture)
                });
            }

            return dataset;
        }


        private static double GetAverageMoldRisk() {
            throw new NotImplementedException();
        }
        //private static double GetAverage<T>(IEnumerable<T> values) where T : IEnumerable<T> {
        //    return values.Sum(x => x) / values.Count;
        //}
        private static double GetAverageMoisture(List<int> moistures) {
            return (double)moistures.Sum(x => x) / moistures.Count;
        }

        private static double GetAverageTemperature(List<double> temperatures) {
            return temperatures.Sum(x => x) / temperatures.Count;
        }

        private static bool IsMonthlyDatacomplete(int month, int daysWithData) {
            double threshold = 0.5D;
            int TotalDaysInMonth = DateTime.DaysInMonth(2016, month);
            return (double)daysWithData / TotalDaysInMonth >= threshold;
        }
        private static bool ValidateDate(string date) {
            return DateTime.TryParse(date, out _);

        }
        private static bool ValidateTime(string time) {
            return TimeOnly.TryParse(time, out _);
        }
        private static bool ValidateTemperature(Match match) {
            return true;
        }
        private static bool ValidateMoisture(Match match) {
            return true;
        }

        internal void GenerateAggregatedData() {
            ConstructData();
        }
        internal void SaveAggregatedData() {

        }
        internal DailyData GetStatistics(DateOnly date) {


            return new DailyData { };
        }
    }
}
