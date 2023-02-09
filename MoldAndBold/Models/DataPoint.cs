
using MoldAndBold.Enums;

namespace MoldAndBold.Models {
    internal class DataPoint {
        internal DateTime Date { get; set; }
        internal double Temperature { get; set; }
        internal int Moisture { get; set; }
        internal Location Location { get; set; }
    }
}
