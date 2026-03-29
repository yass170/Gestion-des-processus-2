namespace ConsoleHelloWorld.Abstractions;

public interface IConsole
{
    void WriteLine(string message);

    void WriteErrorLine(string message);
}
