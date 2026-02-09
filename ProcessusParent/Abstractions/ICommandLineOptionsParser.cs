using ProcessusParent.Application;

namespace ProcessusParent.Abstractions;

public interface ICommandLineOptionsParser
{
    CommandLineOptions Parse(string[] args);
}
