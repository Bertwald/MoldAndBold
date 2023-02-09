
namespace MoldAndBold.Models {
    internal enum Location {
        None,
        Inne,
        Ute
    }
    internal class DataPoint {
        internal DateTime Date { get; set; }
        internal double Temperature { get; set; }
        internal int Moisture { get; set; }
        internal Location Location { get; set; }
    }
}
