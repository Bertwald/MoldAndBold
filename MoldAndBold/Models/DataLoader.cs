using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MoldAndBold.Models
{
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

        internal static void LoadData()
        {
            // StreamReader can fail
            try {
                using (StreamWriter writer = new ("../../../" + "LocalOnly/AggregatedMonths.txt"))
                using (StreamReader reader = new ("../../../" + "LocalOnly/tempdata5-med fel.txt")) {
                    //string regexString11 = """(?<date>\d{4}-\d{2}-\d{2}) (?<time>\d{2}:\d{2}:\d{2}),(?<location>Inne|Ute),(?<temp>-?\d{1,2}.\d{1,2}),(?<moisture>\d{1,2})""";
                    string regexString = "(?<date>\\d{4}-\\d{2}-\\d{2}) (?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>-?\\d{1,2}.\\d{1,2}),(?<moisture>\\d{1,2})";
                    Regex regex = new("(?<date>\\d{4}-\\d{2}-\\d{2}) (?<time>\\d{2}:\\d{2}:\\d{2}),(?<location>Inne|Ute),(?<temp>-?\\d{1,2}.\\d{1,2}),(?<moisture>\\d{1,2})");


                    string rows = reader.ReadToEnd();
                    var test = rows.Split(Environment.NewLine).GroupBy(x => x[..10]).ToList();
                    foreach(var t in test) {
                        Console.WriteLine($"{t.Key}: Has values");
                        // Loop and write values
                        // foreach()
                    }

                    //var matches = regex.Matches(rows).GroupBy(x => x.Groups["date"]).Select(x => x.First()).ToList();

                    //foreach (Match match in Regex.Matches(rows, regexString))
                    //{
                    //    if (ValidateDateInput(match))
                    //    {
                    //        foreach (Group group in match.Groups)
                    //        {
                    //            Console.WriteLine(group.Name + " " + group.Value);
                    //        }

                    //        Console.WriteLine(match.Name);
                    //    }
                    //}

                }
            }
            catch (Exception e) {
                Console.WriteLine("A File could not be used:");
                Console.WriteLine(e.Message);
            }
        }

        private static bool ValidateDate(Match match)
        {
            return DateTime.TryParse(match.Groups["date"].ToString(), out _);

        }
        private static bool ValidateTime(Match match) {
            return true;
        }
        private static bool ValidateTemperature(Match match) {
            return true;
        }
        private static bool ValidateMoisture(Match match) {
            return true;
        }

        internal void GenerateAggregatedData()
        {
            LoadData();
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
