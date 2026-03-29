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

    public void Clear()
    {
        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // No-op when output is redirected.
        }
        catch (InvalidOperationException)
        {
            // No-op when no interactive console is attached.
        }
    }

    public ConsoleKeyInfo ReadKey(bool intercept)
    {
        return Console.ReadKey(intercept);
    }
}
