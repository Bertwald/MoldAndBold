using MoldAndBold.Logic;
using MoldAndBold.Models;

namespace MoldAndBold {
    internal class Program {
        static void Main(string[] args) {
            Console.CursorVisible = false;
            ActionSelector.GetItemFromList(new List<Action> { DataLoader.ConstructData, DataLoader.ConstructData, DataLoader.ConstructData, DoSomething });

            DataLoader.ConstructData();
        }
        private static void DoSomething() {
            return;
        }
    }

}