namespace MoldAndBold.GUI
{
    internal class Menu
    {
        private readonly string _title;
        private readonly string[] _actions;
        private readonly int _maxStringLength;
        private readonly int _boxLength;
        private readonly int _extraLength;
        private readonly ConsoleColor _borderColor = ConsoleColor.Yellow;
        private readonly ConsoleColor _highlightColor;
        private readonly ConsoleColor _textColor = ConsoleColor.DarkBlue;
        private int _highlightedIndex;
        internal Menu(string title, List<string> actions, int highlight = 0, ConsoleColor highlightColor = ConsoleColor.Green)
        {
            _title = "Select " + title;
            _actions = actions.ToArray();
            _highlightedIndex = highlight;
            _highlightColor = highlightColor;
            _maxStringLength = Math.Max(actions.Max(s => s.Length), title.Length);
            _extraLength = (int)Math.Log10(actions.Count); //For very large Lists
            _boxLength = _maxStringLength + 2 + _extraLength; //Junkspaces, box and comma
        }
        private void ShowNumberedActionsMenu()
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = _borderColor;
            Console.Write('╔' + new string('═', _title.Length + 2) + '╗' + Environment.NewLine +
                          '║');
            Console.ForegroundColor = _highlightColor;
            Console.Write($" {_title} ");
            Console.ForegroundColor = _borderColor;
            Console.Write("║" + Environment.NewLine +
                          '╠' + new string('═', _title.Length + 2) + (_title.Length < _maxStringLength ? '╩' + new string('═', _boxLength - _title.Length - 3) + '╗' + Environment.NewLine : '╣' + Environment.NewLine));
            for (int index = 0; index < _actions.Length; index++)
            {
                Console.Write('║');
                Console.ForegroundColor = index == _highlightedIndex ? _highlightColor : Console.ForegroundColor = _textColor;
                Console.Write($" {_actions[index]} " + new string(' ', _boxLength - _actions[index].Length - 2));
                Console.ForegroundColor = _borderColor;
                Console.Write('║' + Environment.NewLine);
            }
            Console.WriteLine('╚' + new string('═', _boxLength) + '╝');
        }
        internal int RunMenu()
        {
            int index = -1;
            Console.Clear();
            while (index < 0)
            {
                ShowNumberedActionsMenu();
                index = PerformAction(Console.ReadKey(true).Key);
            }
            Console.ResetColor();
            return index;
        }
        private int PerformAction(ConsoleKey key) => key switch
        {
            ConsoleKey.UpArrow => ChangeActiveChoice(-1),
            ConsoleKey.DownArrow => ChangeActiveChoice(1),
            ConsoleKey.Enter => _highlightedIndex,
            _ => -1
        };
        private int ChangeActiveChoice(int step)
        {
            _highlightedIndex = (_highlightedIndex + step + _actions.Length) % _actions.Length;
            return -1;
        }

    }
}
