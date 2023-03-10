using MoldAndBold.Enums;
using MoldAndBold.Logic;

namespace MoldAndBold.Models {
    public class DailyData {
        public DateOnly Date { get; set; }
        public double AverageTemperature { get; set; }
        public double AverageMoldRisk { get; set; }
        public double AverageMoisture { get; set; }
        public Location Location { get; set; }
    }
}
