using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.Models {
    internal class DailyData {
        internal DateOnly Date { get; set; }
        // internal double HighTemperature { get; set; }
        // internal double LowTemperature { get; set; }
        internal double AverageTemperature { get; set; }
        internal double AverageMoldRisk { get; set; }
        internal double AverageMoisture { get; set; }
    }
}
