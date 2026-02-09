using System.Threading;

// Validate required arguments.
if (args.Length != 2)
{
    Console.Error.WriteLine("Usage: ConsoleHelloWorld <name> <delay_ms>");
    Environment.ExitCode = 1;
    return;
}

// Read and validate arguments.
string name = args[0];
if (!int.TryParse(args[1], out int delayMs) || delayMs < 0)
{
    Console.Error.WriteLine("Error: <delay_ms> must be a non-negative integer.");
    Environment.ExitCode = 1;
    return;
}

// Build and print the greeting.
string message = BuildGreeting(name);
Console.WriteLine(message);

// Keep the console open for the requested duration.
Thread.Sleep(delayMs);

// Generate the greeting text.
static string BuildGreeting(string name)
{
    return $"Hello {name}";
}
