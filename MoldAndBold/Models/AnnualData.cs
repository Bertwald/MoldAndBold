
namespace MoldAndBold.Models {
    internal class AnnualData {
        internal int Year { get; set; }
        internal DateOnly? AutumnArrival { get; set; }
        internal DateOnly? WinterArrival { get; set; }
        internal double AverageTemperature { get; set; }
        internal double AverageMoisture { get; set; }
        internal double AverageMoldRisk { get; set; }
        internal List<MonthlyData> Months { get; set; } = new();
        internal Location Location { get; set; }

        internal AnnualData(List<MonthlyData> months, DateOnly? autumnArrival, DateOnly? winterArrival ) {
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
