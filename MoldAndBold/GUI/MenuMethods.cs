using MoldAndBold.Logic;
using MoldAndBold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MoldAndBold.GUI
{
    internal static class MenuMethods
    {
        internal static void SearchByDate()
        { }
        internal static void ShowOrderedBy(Func<AnnualData, double> ordning)
        {
            // list<DailyData> data = loadAll();
            var ordered = DataLoader.LoadAllDays(Enums.Location.Inside).OrderBy(ordning);
            // Print
        }
        internal static void ExitProgram()
        {
            Environment.Exit(0);
        }
    }
}
