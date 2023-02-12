using MoldAndBold.Enums;
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

        internal static void ShowOrderedBy(Func<DailyData, double> ordering, Location location, string orderedBy)
        {
            var ordered = (DataLoader.LoadAllDays(location).ExtractDays()).OrderBy(ordering).ToList();
            Helper.ShowDailyData(ordered, orderedBy);
        }

        internal static void ExitProgram()
        {
            Environment.Exit(0);
        }
        internal static void Return() {
            ; // Do Nothing
        }
    }
}
