
namespace MoldAndBold.Logic {
    internal class ActionSelector {
        internal static T? GetItemFromList<T>(List<T> items) where T : Delegate {
            if (!items.Any()) {
                return null;
            }
            Menu indexMenu = new(typeof(T).Name,
                           items.Select(x => x.AsString()).ToList()!,
                           0);
            int index = indexMenu.RunMenu();
            return items[index];
        }
    }
}
