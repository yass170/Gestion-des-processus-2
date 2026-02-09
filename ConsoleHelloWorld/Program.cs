using System.Threading;

if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: ConsoleHelloWorld <name> <delay_ms>");
    Environment.ExitCode = 1;
    return;
}

string name = args[0];
if (!int.TryParse(args[1], out int delayMs) || delayMs < 0)
{
    Console.Error.WriteLine("Error: <delay_ms> must be a non-negative integer.");
    Environment.ExitCode = 1;
    return;
}

string message = BuildGreeting(name);
Console.WriteLine(message);

Thread.Sleep(delayMs);

static string BuildGreeting(string name)
{
    return $"Hello {name}";
}
