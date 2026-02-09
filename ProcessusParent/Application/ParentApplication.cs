using ProcessusParent.Abstractions;

namespace ProcessusParent.Application;

public sealed class ParentApplication
{
    private readonly IConsole _console;
    private readonly ICommandLineOptionsParser _optionsParser;
    private readonly IProcessListPresenter _processListPresenter;
    private readonly IParentWorkflow _parentWorkflow;

    public ParentApplication(
        IConsole console,
        ICommandLineOptionsParser optionsParser,
        IProcessListPresenter processListPresenter,
        IParentWorkflow parentWorkflow)
    {
        _console = console;
        _optionsParser = optionsParser;
        _processListPresenter = processListPresenter;
        _parentWorkflow = parentWorkflow;
    }

    public int Run(string[] args)
    {
        CommandLineOptions options = _optionsParser.Parse(args);
        if (!options.IsValid)
        {
            _console.WriteErrorLine(options.ValidationMessage ?? "Invalid arguments.");
            return 1;
        }

        if (options.Mode == ApplicationMode.ListProcesses)
        {
            _processListPresenter.PrintRunningProcesses();
            return 0;
        }

        return _parentWorkflow.RunDefaultScenario();
    }
}
