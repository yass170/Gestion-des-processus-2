using ProcessusParent.Abstractions;

namespace ProcessusParent.Infrastructure;

public sealed class SystemConsole : IConsole
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteErrorLine(string message)
    {
        Console.Error.WriteLine(message);
    }
}
