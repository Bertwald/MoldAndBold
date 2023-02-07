using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
