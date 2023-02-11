using static MoldAndBold.Logic.DataLoader;
using MoldAndBold.Enums;
using static MoldAndBold.GUI.MenuMethods;
using MoldAndBold.Logic;
using static MoldAndBold.GUI.InsideMenuMethods;
using static MoldAndBold.GUI.OutsideMenuMethods;

namespace MoldAndBold {
    internal class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            while (true) {
                ActionSelector.ExecuteActionFromList(new List<Action> { ShowInsideData, ShowOutsideData, ConstructData, ExitProgram });
            }
            //var data = DataLoader.LoadAllDays(Location.Inside);
            //DataLoader.ConstructData();
        }
    }

}