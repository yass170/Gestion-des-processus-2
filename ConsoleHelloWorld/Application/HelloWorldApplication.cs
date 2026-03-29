using ConsoleHelloWorld.Abstractions;

namespace ConsoleHelloWorld.Application;

public sealed class HelloWorldApplication
{
    private readonly IConsole _console;
    private readonly IGreetingService _greetingService;
    private readonly IExecutionDelay _executionDelay;

    public HelloWorldApplication(
        IConsole console,
        IGreetingService greetingService,
        IExecutionDelay executionDelay)
    {
        _console = console;
        _greetingService = greetingService;
        _executionDelay = executionDelay;
    }

    public int Run(string[] args)
    {
        if (args.Length != 2)
        {
            _console.WriteErrorLine("Usage: ConsoleHelloWorld <name> <delay_ms>");
            return 1;
        }

        string name = args[0];
        if (!int.TryParse(args[1], out int delayMs) || delayMs < 0)
        {
            _console.WriteErrorLine("Error: <delay_ms> must be a non-negative integer.");
            return 1;
        }

        string message = _greetingService.BuildGreeting(name);
        _console.WriteLine(message);
        _executionDelay.Wait(delayMs);

        return 0;
    }
}
