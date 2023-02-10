
using MoldAndBold.Enums;
using System.Text.Json.Serialization;

namespace MoldAndBold.Models {
    internal class AnnualData {
        public int Year { get; set; }
        public DateOnly? AutumnArrival { get; set; }
        public DateOnly? WinterArrival { get; set; }
        public double AverageTemperature { get; set; }
        public double AverageMoisture { get; set; }
        public double AverageMoldRisk { get; set; }
        public List<MonthlyData> Months { get; set; } = new();
        public Location Location { get; set; }
        [JsonConstructor]
        public AnnualData() { }
        public AnnualData(List<MonthlyData> months, DateOnly? autumnArrival, DateOnly? winterArrival ) {
            Months = months;
            Location = months[0].Location;
            Year = months[0].Days[0].Date.Year;
            AverageTemperature = months.Select(x => x.AverageTemperature).Average();
            AverageMoisture = months.Select(x => x.AverageMoisture).Average();
            AverageMoldRisk = months.Select(x => x.AverageMoldRisk).Average();
            AutumnArrival= autumnArrival;
            WinterArrival= winterArrival;
        }

    }
}
