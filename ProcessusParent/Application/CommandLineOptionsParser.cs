using ProcessusParent.Abstractions;

namespace ProcessusParent.Application;

public sealed class CommandLineOptionsParser : ICommandLineOptionsParser
{
    private const string ListProcessesArgument = "--list-processes";

    public CommandLineOptions Parse(string[] args)
    {
        if (args.Length == 0)
        {
            return CommandLineOptions.CreateValid(ApplicationMode.Demo);
        }

        if (args.Length == 1 && args[0].Equals(ListProcessesArgument, StringComparison.OrdinalIgnoreCase))
        {
            return CommandLineOptions.CreateValid(ApplicationMode.ListProcesses);
        }

        return CommandLineOptions.CreateInvalid(
            "Invalid arguments. Use '--list-processes' or run without arguments.");
    }
}
