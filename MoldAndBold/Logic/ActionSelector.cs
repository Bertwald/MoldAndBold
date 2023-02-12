using MoldAndBold.GUI;

namespace MoldAndBold.Logic
{
    internal class ActionSelector {
        internal static void ExecuteActionFromList<T>(List<T> items) where T : Delegate {
            if (!items.Any()) {
                return;
            }
            Menu indexMenu = new(typeof(T).Name,
                           items.Select(x => x.AsString()).ToList()!,
                           0);
            int index = indexMenu.RunMenu();
            items[index].DynamicInvoke();
        }
    }
}
