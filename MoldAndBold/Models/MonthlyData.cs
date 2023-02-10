using MoldAndBold.Enums;
using System.Text.Json.Serialization;

namespace MoldAndBold.Models {
    public class MonthlyData {
        public int Year { get; set; }
        public Month Month { get; set; }
        public double AverageTemperature { get; set; }
        public double AverageMoisture { get; set; }
        public double AverageMoldRisk { get; set; }
        public List<DailyData> Days { get; set; } = new();
        public Location Location { get; set; }
        public bool CompleteData { get; }
        [JsonConstructor]
        public MonthlyData() { }
        public MonthlyData(List<DailyData> mdays) {
            Year = mdays[0].Date.Year;
            Month = (Month)mdays[0].Date.Month;
            Days = mdays;
            Location = mdays[0].Location;
            AverageMoisture = mdays.Select(x => x.AverageMoisture).Average();
            AverageTemperature = mdays.Select(x => x.AverageTemperature).Average();
            CompleteData = IsMonthlyDatacomplete(mdays[0].Date.Month, mdays.Count);
        } 

    private static bool IsMonthlyDatacomplete(int month, int daysWithData) {
        double threshold = 0.5D;
        int TotalDaysInMonth = DateTime.DaysInMonth(2016, month);
        return (double)daysWithData / TotalDaysInMonth >= threshold;
    }
    }
}
