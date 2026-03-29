using ConsoleHelloWorld.Abstractions;

namespace ConsoleHelloWorld.Infrastructure;

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
