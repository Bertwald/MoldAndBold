using MoldAndBold.Logic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MoldAndBold.Models {
    internal class DataLoader
    {

        internal static List<DataPoint> GetDailyData()
        {

            // Check if data exists in aggregated file
            // If No
            // Check if "tempdata5-med fel.txt" is present and can be read
            // Yes, Generate and save aggregate data
            // No, Fail Panic and blame someone else
            // If Yes
            // Load contents and return

            return new List<DataPoint>();
        }

        internal static void ConstructData()
        {
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

            //Console.WriteLine($"Double avg: {(new List<double> { 1.0D, 1.0D, 8.0D }).GetMeanTypeValue()}");
            //Console.WriteLine($"Integer avg: {(new List<int> { 1, 1, 8 }).GetMeanTypeValue()}");


            List<DataPoint> dataset = ConstructDataset(rows);
            dataset = PurgeDateDataPoints(dataset, new DateTime(2015, 06, 01), new DateTime(2018, 01, 01));
            Console.WriteLine($"Number of datapoints: {dataset.Count}");

            // TODO: Purge Datapoints from invalid moisture data (framtidssäkra)

            (List<DataPoint> inside, List<DataPoint> outside) = dataset.Split(x => x.Location == Location.Inne);
            var insideDailyData = PurgeTemperatureDataPoints(inside).GroupBy(x => x.Date.Date);
            var outsideDailyData = PurgeTemperatureDataPoints(outside).GroupBy(x => x.Date.Date);

            var daysInside = ConstructDailyData(insideDailyData);
            var daysOutside = ConstructDailyData(outsideDailyData);

            Console.WriteLine($"Number of datapoints inside: {inside.Count}");
            Console.WriteLine($"Number of datapoints outside: {outside.Count}");

            Console.WriteLine($"Inside Maxii : {inside.Select(x => x.Temperature).Max()}    {inside.Select(x => x.Moisture).Max()}");
            Console.WriteLine($"Outside Maxii : {outside.Select(x => x.Temperature).Max()}    {outside.Select(x => x.Moisture).Max()}");

            Console.WriteLine($"Inside Minimii : {inside.Select(x => x.Temperature).Min()}    {inside.Select(x => x.Moisture).Min()}");
            Console.WriteLine($"Outside Minimii: {outside.Select(x => x.Temperature).Min()}    {outside.Select(x => x.Moisture).Min()}");

            Console.WriteLine($"Inside avg: {inside.Select(x => x.Temperature).Average()}");
            Console.WriteLine($"Outside avg: {outside.Select(x => x.Temperature).Average()}");

            List<DailyData> winterDays = daysOutside.Where(x => x.AverageTemperature < 0).ToList();
            List<DailyData> autumnDays = daysOutside.Where(x => x.AverageTemperature < 10 && x.Date >= new DateOnly(2016, 08, 01)).ToList();

            var autumnDate = GetSwedishMeteorologicalAutumn(autumnDays);
            var winterDate = FiveDaysInARow(winterDays);

            IEnumerable<List<DailyData>> InsideGroupedByMonth = daysInside.GroupBy(x => x.Date.Month).Select(x => x.ToList());
            IEnumerable<List<DailyData>> OutsideGroupedByMonth = daysOutside.GroupBy(x => x.Date.Month).Select(x => x.ToList());

            var monthsInside = (from m in InsideGroupedByMonth let mdays = m.ToList() select new MonthlyData(mdays)).ToList().GroupBy(x => x.Year);
            var monthsOutside = (from m in OutsideGroupedByMonth let mdays = m.ToList() select new MonthlyData(mdays)).ToList().GroupBy(x => x.Year);

            var yearsInside = (from m in monthsInside let monthsInsideAtYear = m.ToList() select new AnnualData(monthsInsideAtYear, autumnDate, winterDate));
            var yearsOutside = (from m in monthsOutside let monthsOutsideAtYear = m.ToList() select new AnnualData(monthsOutsideAtYear, autumnDate, winterDate));





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

        private static DateOnly GetSwedishMeteorologicalAutumn(List<DailyData> autumnDays)
        {
            return FiveDaysInARow(autumnDays) ?? new DateOnly(2017, 02, 14);
            
        }

        private static DateOnly? FiveDaysInARow(List<DailyData> days)
        {
            for (int start = 0; start < days.Count - 5; start++)
            {
                for (int offset = 1; offset < 5; offset++)
                {
                    if (days[start].Date.AddDays(offset) != days[start + offset].Date)
                    {
                        break;
                    }
                    if (offset == 4)
                    {
                        return days[start].Date;
                    }
                }
            }
            return null;
        }

        private static List<DailyData> ConstructDailyData(IEnumerable<IGrouping<DateTime, DataPoint>> dailyDataPoints)
        {
            var dailyDatas = new List<DailyData>();
            foreach (var day in dailyDataPoints)
            {
                var avarageTemperature = day.Select(x => x.Temperature).Average();
                var avarageMoisture = day.Select(x => x.Moisture).Average();
                dailyDatas.Add(new DailyData() {
                    Date = DateOnly.FromDateTime(day.Key),
                    AverageTemperature = avarageTemperature,
                    AverageMoisture = avarageMoisture,
                    AverageMoldRisk = GetMoldRisk(avarageTemperature, avarageMoisture),
                    Location = day.ToList()[0].Location
                }) ;

            }
            //dailyDatas.Add(new DailyData()
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now),
            //    AverageTemperature = 40,
            //    AverageMoisture = 85,
            //    AverageMoldRisk = GetMoldRisk(40, 85)
            //});
            return dailyDatas;
        }

        private static List<DataPoint> PurgeTemperatureDataPoints(List<DataPoint> list)
        {
            // TODO: If time, repare faulted data
            if (list[0].Location == Location.Inne)
                return list.Where(x => x.Temperature < 40 && x.Temperature > 16).ToList();
            else
                return list.Where(x => x.Temperature < 50 && x.Temperature > -30).ToList();
        }

        private static List<DataPoint> PurgeDateDataPoints(List<DataPoint> list, DateTime startDate, DateTime endDate)
        {
            return list.Where(x => x.Date < endDate && x.Date >= startDate).ToList();
        }

        private static void ConstructOutsideSeries(List<DataPoint> outside)
        {
            throw new NotImplementedException();
        }

        private static void ConstructInsideSeries(List<DataPoint> inside)
        {
            throw new NotImplementedException();
        }

        private static string LoadTempData()
        {
            try
            {
                string rows;
                using (StreamReader reader = new("../../../" + "LocalOnly/tempdata5-med fel.txt"))
                {
                    rows = reader.ReadToEnd();
                }
                return rows;
            }
            catch (Exception e)
            {
                Console.WriteLine("A File could not be used:");
                Console.WriteLine(e.Message);
            }
            return String.Empty;
        }

        private static List<DataPoint> ConstructDataset(string rows)
        {
            Regex regex = new("(?<date>\\d{4}-\\d{2}-\\d{2}) (?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>-?\\d{1,2}.\\d{1,2}),(?<moisture>\\d{1,2})");
            List<DataPoint> dataset = new();
            foreach (Match match in regex.Matches(rows).Where(x => ValidateDate(x.Groups["date"].Value) && ValidateTime(x.Groups["time"].Value)))
            {
                dataset.Add(new DataPoint()
                {
                    Date = DateTime.Parse(match.Groups["date"].Value + " " + match.Groups["time"].Value),
                    Location = (Location)Enum.Parse(typeof(Location), match.Groups["location"].Value),
                    Moisture = int.Parse(match.Groups["moisture"].Value),
                    Temperature = double.Parse(match.Groups["temp"].Value, CultureInfo.InvariantCulture)
                });
            }

            return dataset;
        }


        private static double GetMoldRisk(double temperature, double moisture)
        {
            if (temperature < 0 || temperature > 50 || moisture < 60)
            {
                return 0;
            }
            else
            {
                return Math.Max(100 - 1.5 * (100 - moisture) - ((temperature > 30 ? 1.5 : 1) * (Math.Abs(30 - temperature))) - (Math.Abs(30 - temperature)) * (moisture) / 100, 0);
            }
        }
        //private static double GetAverage<T>(IEnumerable<T> values) where T : IEnumerable<T> {
        //    return values.Sum(x => x) / values.Count;
        //}
        private static double GetAverageMoisture(List<int> moistures)
        {
            return (double)moistures.Sum(x => x) / moistures.Count;
        }

        private static double GetAverageTemperature(List<double> temperatures)
        {
            return temperatures.Sum(x => x) / temperatures.Count;
        }

        private static bool IsMonthlyDatacomplete(int month, int daysWithData)
        {
            double threshold = 0.5D;
            int TotalDaysInMonth = DateTime.DaysInMonth(2016, month);
            return (double)daysWithData / TotalDaysInMonth >= threshold;
        }
        private static bool ValidateDate(string date)
        {
            return DateTime.TryParse(date, out _);

        }
        private static bool ValidateTime(string time)
        {
            return TimeOnly.TryParse(time, out _);
        }
        private static bool ValidateTemperature(Match match)
        {
            return true;
        }
        private static bool ValidateMoisture(Match match)
        {
            return true;
        }

        internal void GenerateAggregatedData()
        {
            ConstructData();
        }
        internal void SaveAggregatedData()
        {

        }
        internal DailyData GetStatistics(DateOnly date)
        {


            return new DailyData { };
        }
    }
}
