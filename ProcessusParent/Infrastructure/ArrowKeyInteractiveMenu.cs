using ProcessusParent.Abstractions;

namespace ProcessusParent.Infrastructure;

public sealed class ArrowKeyInteractiveMenu : IInteractiveMenu
{
    private readonly IConsole _console;

    public ArrowKeyInteractiveMenu(IConsole console)
    {
        _console = console;
    }

    public int PromptSelection(string title, IReadOnlyList<string> options)
    {
        if (options.Count == 0)
        {
            throw new ArgumentException("At least one menu option is required.", nameof(options));
        }

        int selectedIndex = 0;

        while (true)
        {
            DrawMenu(title, options, selectedIndex);
            ConsoleKeyInfo keyInfo = _console.ReadKey(intercept: true);

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIndex = selectedIndex == 0 ? options.Count - 1 : selectedIndex - 1;
                continue;
            }

            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIndex = selectedIndex == options.Count - 1 ? 0 : selectedIndex + 1;
                continue;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                return selectedIndex;
            }
        }
    }

    private void DrawMenu(string title, IReadOnlyList<string> options, int selectedIndex)
    {
        _console.Clear();
        _console.WriteLine(title);
        _console.WriteLine("Use Up/Down arrows and press Enter.");
        _console.WriteLine(string.Empty);

        for (int index = 0; index < options.Count; index++)
        {
            string prefix = index == selectedIndex ? "> " : "  ";
            _console.WriteLine($"{prefix}{options[index]}");
        }
    }
}
