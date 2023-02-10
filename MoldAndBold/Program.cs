using static MoldAndBold.Logic.DataLoader;
using MoldAndBold.Enums;
using static MoldAndBold.GUI.ConsoleUI;
using MoldAndBold.Logic;

namespace MoldAndBold
{
    internal class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            ActionSelector.GetItemFromList(new List<Action> { ShowInsideData, ShowOutsideData, ConstructData });
            ActionSelector.GetItemFromList(new List<Action> { SearchByDate, ShowDaysOrderedByTemp, ShowDaysOrderedByHumidity, ShowDaysOrderedByMoldRisk });
            //var data = DataLoader.LoadAllDays(Location.Inside);
            //DataLoader.ConstructData();
        }
        private static void DoSomething() {
            return;
        }
    }

}