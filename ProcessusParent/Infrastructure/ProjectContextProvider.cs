using ProcessusParent.Abstractions;
using ProcessusParent.Domain;
using System.Linq;

namespace ProcessusParent.Infrastructure;

public sealed class ProjectContextProvider : IProjectContextProvider
{
    private const string SolutionFileName = "Gestion-des-processus-2.sln";
    private const string TargetFramework = "net10.0";

    private readonly ISolutionLocator _solutionLocator;

    public ProjectContextProvider(ISolutionLocator solutionLocator)
    {
        _solutionLocator = solutionLocator;
    }

    public bool TryGetProjectContext(out ProjectContext? context, out string errorMessage)
    {
        if (!_solutionLocator.TryFindSolutionRoot(AppContext.BaseDirectory, SolutionFileName, out string solutionRoot))
        {
            context = null;
            errorMessage = "Solution root not found. Run the app from inside the repository.";
            return false;
        }

        string[] helloWorldCandidates =
        {
            Path.Combine(solutionRoot, "ConsoleHelloWorld", "bin", "Debug", TargetFramework, "ConsoleHelloWorld.exe"),
            Path.Combine(solutionRoot, "ConsoleHelloWorld", "bin", "Release", TargetFramework, "ConsoleHelloWorld.exe")
        };

        string? existingPath = helloWorldCandidates.FirstOrDefault(File.Exists);
        if (existingPath is null)
        {
            context = null;
            errorMessage = "ConsoleHelloWorld executable not found. Build ConsoleHelloWorld first.";
            return false;
        }

        context = new ProjectContext(solutionRoot, existingPath);
        errorMessage = string.Empty;
        return true;
    }
}
