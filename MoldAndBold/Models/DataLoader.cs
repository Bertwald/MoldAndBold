using MoldAndBold.Enums;
using MoldAndBold.Logic;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

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

            List<DataPoint> dataSet = ConstructDataSet(rows);
            (var insideYears, var outsideYears) = GenerateAggregatedData(dataSet);

            GenerateJson(insideYears);

            GenerateReportFile(insideYears, outsideYears);
            GenerateJsonFiles(insideYears, outsideYears);
        }

        private static void GenerateJsonFiles(IEnumerable<AnnualData> insideYears, IEnumerable<AnnualData> outsideYears) {
            throw new NotImplementedException();
        }

        private static void GenerateReportFile(IEnumerable<AnnualData> insideYears, IEnumerable<AnnualData> outsideYears) {
            string path = ;
            string filename = "report.txt";

            string content = "ReportFile" + Environment.NewLine;
            content += "Outdoors Statistics" + Environment.NewLine;
            foreach (AnnualData data in outsideYears) {
                content += "First autumn day: " + data.AutumnArrival + Environment.NewLine;
                content += "First winter day: " + data.WinterArrival + Environment.NewLine;
                foreach(MonthlyData month in data.Months) {
                    content += month.Month.ToString();
                    content += "Average temperature: " + month.AverageTemperature + Environment.NewLine;
                    content += "Average Moisture: " + month.AverageMoisture + Environment.NewLine;
                    content += "Mold Risk: " + month.AverageMoldRisk + Environment.NewLine;
                }
            }
            foreach (AnnualData data in insideYears) {
                content += "Indoors Statistics" + Environment.NewLine;
                foreach (MonthlyData month in data.Months) {
                    content += "" + month.AverageTemperature + Environment.NewLine;
                    content += "" + month.AverageMoisture + Environment.NewLine;
                    content += "" + month.AverageMoldRisk + Environment.NewLine;
                }
            }
        }

        private static string GenerateJson(IEnumerable<AnnualData> insideYears) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
            return JsonSerializer.Serialize(insideYears, options);
            //return JsonSerializer.Deserialize<List<AnnualData>>(jsonString, options)!;
        }

        private static DateOnly GetSwedishMeteorologicalAutumn(List<DailyData> autumnDays) {
            // TODO: Remove magic numbers
            return FiveDaysInARow(autumnDays) ?? new DateOnly(2017, 02, 14);
        }

        private static DateOnly? FiveDaysInARow(List<DailyData> days) {
            for (int start = 0; start < days.Count - 5; start++) {
                for (int offset = 1; offset < 5; offset++) {
                    if (days[start].Date.AddDays(offset) != days[start + offset].Date) {
                        break;
                    }
                    if (offset == 4) {
                        return days[start].Date;
                    }
                }
            }
            return null;
        }

        private static List<DailyData> ConstructDailyData(IEnumerable<IGrouping<DateTime, DataPoint>> dailyDataPoints) {
            var dailyDatas = new List<DailyData>();
            foreach (var day in dailyDataPoints) {
                var avarageTemperature = day.Select(x => x.Temperature).Average();
                var avarageMoisture = day.Select(x => x.Moisture).Average();
                dailyDatas.Add(new DailyData() {
                    Date = DateOnly.FromDateTime(day.Key),
                    AverageTemperature = avarageTemperature,
                    AverageMoisture = avarageMoisture,
                    AverageMoldRisk = GetMoldRisk(avarageTemperature, avarageMoisture),
                    Location = day.ToList()[0].Location
                });
            }

            /*
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(dailyDatas, options);

            Console.WriteLine(jsonString);
            */
            return dailyDatas;
        }

        private static List<DataPoint> PurgeTemperatureDataPoints(List<DataPoint> list) {
            // TODO: If time, repare faulted data and remove magic numbers
            if (list[0].Location == Location.Inne)
                return list.Where(x => x.Temperature < 40 && x.Temperature > 16).ToList();
            else
                return list.Where(x => x.Temperature < 50 && x.Temperature > -30).ToList();
        }

        private static List<DataPoint> PurgeDateDataPoints(List<DataPoint> list, DateTime startDate, DateTime endDate) {
            return list.Where(x => x.Date < endDate && x.Date >= startDate).ToList();
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

        private static List<DataPoint> ConstructDataSet(string rows) {
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

        private static double GetMoldRisk(double temperature, double moisture) {
            if (temperature < 0 || temperature > 50 || moisture < 60) {
                return 0;
            } else {
                return Math.Max(100 - 1.5 * (100 - moisture) - ((temperature > 30 ? 1.5 : 1) * (Math.Abs(30 - temperature))) - (Math.Abs(30 - temperature)) * (moisture) / 100, 0);
            }
        }

        private static bool ValidateDate(string date) {
            return DateTime.TryParse(date, out _);
        }

        private static bool ValidateTime(string time) {
            return TimeOnly.TryParse(time, out _);
        }

        internal static (IEnumerable<AnnualData>, IEnumerable<AnnualData>) GenerateAggregatedData(List<DataPoint> dataSet) {
            dataSet = PurgeDateDataPoints(dataSet, new DateTime(2015, 06, 01), new DateTime(2018, 01, 01));

            (var daysInside, var daysOutside) = GenerateDays(dataSet);

            var monthsInside = GenerateMonths(daysInside);
            var monthsOutside = GenerateMonths(daysOutside);

            //
            return GenerateYears(monthsInside, monthsOutside);
        }

        private static (IEnumerable<AnnualData> inside, IEnumerable<AnnualData> outside) GenerateYears(IEnumerable<IGrouping<int, MonthlyData>> monthsInside, IEnumerable<IGrouping<int, MonthlyData>> monthsOutside) {
            List<AnnualData> inside = new List<AnnualData>(), outside = new List<AnnualData>();

            var outsideMonthsGroupedByYear = monthsOutside.GroupBy(x => x.ToList().Select(x => x.Year));
            var insideMonthsGroupedByYear = monthsInside.GroupBy(x => x.ToList().Select(x => x.Year));
            int start = insideMonthsGroupedByYear.SelectMany(x => x.Key).Min();
            int end = insideMonthsGroupedByYear.SelectMany(x => x.Key).Max();
            for (int year = start; year <= end; year++) {
                // Outside Data
                var unionedWinterDays = GetUnionedList(outsideMonthsGroupedByYear, year, 0, new DateOnly(year + 1, 2, 15));
                var unionedAutumnDays = GetUnionedList(outsideMonthsGroupedByYear, year, 10, new DateOnly(year + 1, 2, 15));
                var autumnDate = GetSwedishMeteorologicalAutumn(unionedAutumnDays.Where(x => x.Date > new DateOnly(year, 9, 1)).ToList());
                var winterDate = FiveDaysInARow(unionedWinterDays);
                // TODO: här slutade vi
                //var test = outsideMonthsGroupedByYear.Where(x => x.Key.Contains(year)).Select(x => x).Select(x => x).ToList();
                var outsideMonthsInYear = outsideMonthsGroupedByYear.SelectMany(x => x.SelectMany(x => x.Select(x => x).Where(x => x.Year == year)));
                outside.Add(new AnnualData() {
                    Year = year,
                    AutumnArrival = autumnDate,
                    WinterArrival = winterDate,
                    AverageTemperature = outsideMonthsInYear.Select(x => x.AverageTemperature).Average(),
                    Location = Location.Ute,
                    Months = outsideMonthsInYear.ToList()
                });

                //Inside Data
                var insideMonthsInYear = insideMonthsGroupedByYear.SelectMany(x => x.SelectMany(x => x.Select(x => x).Where(x => x.Year == year)));
                inside.Add(new AnnualData() {
                    Year = year,
                    AverageTemperature = insideMonthsInYear.Select(x => x.AverageTemperature).Average(),
                    Location = Location.Inne,
                    Months = insideMonthsInYear.ToList()
                });

            }

            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };
            string jsonString = JsonSerializer.Serialize(insideMonthsGroupedByYear.ToList().SelectMany(x => x.SelectMany(x => x)).Select(x => x), options);
            Console.WriteLine(jsonString);
            var monthlyDatas = JsonSerializer.Deserialize<List<MonthlyData>>(jsonString, options)!;



            //foreach(var y in insideMonthsGroupedByYear) {
            //    var monthsInYear = y.ToList();
            //    var daysInYear = y.ToList().SelectMany(x => x).SelectMany(x => x.Days);
            //    List<DailyData> winterDays;
            //    List<DailyData> autumnDays;


            //}
            //foreach (var y in outsideMonthsGroupedByYear) {

            //}

            /*
            List<DailyData> winterDays = monthsOutside.GroupBy(x => x.ToList().Select(x => x.Year)) //.Where(x => x.ToList().Select(x => x.ToList().Select(x => x.ToList().Select)) < 0).ToList();
            List<DailyData> autumnDays = daysOutside.Where(x => x.AverageTemperature < 10 && x.Date >= new DateOnly(2016, 08, 01)).ToList();
            */

            //var yearsInside = (from m in monthsInside let monthsInsideAtYear = m.ToList() select new AnnualData(monthsInsideAtYear, DateOnly.FromDayNumber(0), DateOnly.FromDayNumber(0)));
            //var yearsOutside = (from m in monthsOutside let monthsOutsideAtYear = m.ToList() select new AnnualData(monthsOutsideAtYear, DateOnly.FromDayNumber(0), DateOnly.FromDayNumber(0)));
            return (inside, outside);
        }

        private static List<DailyData> GetUnionedList(IEnumerable<IGrouping<IEnumerable<int>, IGrouping<int, MonthlyData>>> outsideMonthsGroupedByYear, int year, int tempRoof, DateOnly endDate) {
            // endDate is exclusive
            return outsideMonthsGroupedByYear
                .Where(x => x.Key.Contains(year) || x.Key.Contains(year + 1))
                .ToList()
                .SelectMany(x => x)
                .Select(x => x.ToList())
                .SelectMany(x => x.SelectMany(x => x.Days.Where(x => x.AverageTemperature < tempRoof && x.Date < endDate)))
                .ToList();

            //var daysInYear = outsideMonthsGroupedByYear.Where(x => x.Key.Contains(year)).ToList().SelectMany(x => x);
            //var winterDaysInCurrenyYear = daysInYear.Select(x => x.ToList()).SelectMany(x => x.SelectMany(x => x.Days.Where(x => x.AverageTemperature < 0))).ToList();
            //var daysInNextYear = outsideMonthsGroupedByYear.Where(x => x.Key.Contains(year + 1)).ToList().SelectMany(x => x);
            //var winterDaysInNextYear = daysInNextYear.Select(x => x.ToList()).SelectMany(x => x.SelectMany(x => x.Days.Where(x => x.AverageTemperature < 0 && x.Date < new DateOnly(year + 1, 2, 15)))).ToList();
            //return winterDaysInCurrenyYear.Union(winterDaysInNextYear).ToList();
        }

        private static IEnumerable<IGrouping<int, MonthlyData>> GenerateMonths(IEnumerable<DailyData> days) {
            IEnumerable<List<DailyData>> groupedByMonth = days.GroupBy(x => x.Date.Month).Select(x => x.ToList());
            return (from m in groupedByMonth let mdays = m.ToList() select new MonthlyData(mdays)).ToList().GroupBy(x => x.Year);
            //IEnumerable<List<DailyData>> InsideGroupedByMonth = daysInside.GroupBy(x => x.Date.Month).Select(x => x.ToList());
            //IEnumerable<List<DailyData>> OutsideGroupedByMonth = daysOutside.GroupBy(x => x.Date.Month).Select(x => x.ToList());

            //var monthsInside = (from m in InsideGroupedByMonth let mdays = m.ToList() select new MonthlyData(mdays)).ToList().GroupBy(x => x.Year);
            //var monthsOutside = (from m in OutsideGroupedByMonth let mdays = m.ToList() select new MonthlyData(mdays)).ToList().GroupBy(x => x.Year);
        }

        private static (IEnumerable<DailyData> inside, IEnumerable<DailyData> outside) GenerateDays(List<DataPoint> dataSet) {
            // TODO: Purge Datapoints from invalid moisture data (framtidssäkra)

            (List<DataPoint> inside, List<DataPoint> outside) = dataSet.Split(x => x.Location == Location.Inne);
            var insideDailyData = PurgeTemperatureDataPoints(inside).GroupBy(x => x.Date.Date);
            var outsideDailyData = PurgeTemperatureDataPoints(outside).GroupBy(x => x.Date.Date);

            var daysInside = ConstructDailyData(insideDailyData);
            var daysOutside = ConstructDailyData(outsideDailyData);
            return (daysInside, daysOutside);
        }



        internal void SaveAggregatedData() {

        }
        internal DailyData GetStatistics(DateOnly date) {


            return new DailyData { };
        }
    }
}
