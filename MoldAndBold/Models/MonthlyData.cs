using MoldAndBold.Enums;

namespace MoldAndBold.Models {
    internal class MonthlyData {
        internal int Year { get; set; }
        internal Month Month { get; set; }
        internal double AverageTemperature { get; set; }
        internal double AverageMoisture { get; set; }
        internal double AverageMoldRisk { get; set; }
        internal List<DailyData> Days { get; set; } = new();
        internal Location Location { get; set; }
        internal bool CompleteData { get; }
        internal MonthlyData(List<DailyData> mdays) {
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
