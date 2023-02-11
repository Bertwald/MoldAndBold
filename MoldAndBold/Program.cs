using static MoldAndBold.Logic.DataLoader;
using MoldAndBold.Enums;
using static MoldAndBold.GUI.MenuMethods;
using MoldAndBold.Logic;

namespace MoldAndBold
{
    internal class Program {
        static void Main(string[] args) {
            //Console.CursorVisible = false;
            //while (true) {
            //    //
            //    ActionSelector.ExecuteActionFromList(new List<Action> { ShowInsideData, ShowOutsideData, ConstructData });
            //}
            //var data = DataLoader.LoadAllDays(Location.Inside);
            DataLoader.ConstructData();
        }
        private static void DoSomething() {
            return;
        }
    }

}