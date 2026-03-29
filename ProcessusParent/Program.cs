using ProcessusParent.Abstractions;
using ProcessusParent.Application;
using ProcessusParent.Infrastructure;

namespace ProcessusParent;

public static class Program
{
    public static int Main(string[] args)
    {
        IConsole console = new SystemConsole();
        ICommandLineOptionsParser optionsParser = new CommandLineOptionsParser();
        IParentProcessResolver parentProcessResolver = new WindowsParentProcessResolver();
        IProcessInspector processInspector = new ProcessInspector(parentProcessResolver);
        IProcessListPresenter processListPresenter = new ProcessListPresenter(console, processInspector);
        ISolutionLocator solutionLocator = new SolutionLocator();
        IProjectContextProvider projectContextProvider = new ProjectContextProvider(solutionLocator);
        ITextFileWriter textFileWriter = new TextFileWriter();
        IProcessLauncher processLauncher = new ProcessLauncher(console);
        IInteractiveMenu interactiveMenu = new ArrowKeyInteractiveMenu(console);
        IParentWorkflow parentWorkflow = new ParentWorkflow(
            console,
            projectContextProvider,
            textFileWriter,
            processLauncher,
            interactiveMenu);

        var application = new ParentApplication(console, optionsParser, processListPresenter, parentWorkflow);
        return application.Run(args);
    }
}
