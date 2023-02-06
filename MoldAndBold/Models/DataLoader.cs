using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.Models {
    internal class DataLoader {

        internal static List<DataPoint> GetDailyData() {

            // Check if data exists in aggregated file
                // If No
                // Check if "tempdata5-med fel.txt" is present and can be read
                   // Yes, Generate and save aggregate data
                   // No, Fail Panic and blame someone else
                // If Yes
                // Load contents and return

            return new List<DataPoint>();
        }

        internal void LoadData() {

        }
        internal void GenerateAggregatedData() {
            LoadData();
        }
        internal void SaveAggregatedData() {

        }
        internal DailyData GetStatistics(DateOnly date) {


            return new DailyData { };
        }
    }
}
