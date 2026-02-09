using ProcessusParent.Abstractions;

namespace ProcessusParent.Infrastructure;

public sealed class SolutionLocator : ISolutionLocator
{
    public bool TryFindSolutionRoot(string startDirectory, string solutionFileName, out string solutionRoot)
    {
        DirectoryInfo? currentDirectory = new DirectoryInfo(startDirectory);

        while (currentDirectory is not null)
        {
            string candidatePath = Path.Combine(currentDirectory.FullName, solutionFileName);
            if (File.Exists(candidatePath))
            {
                solutionRoot = currentDirectory.FullName;
                return true;
            }

            currentDirectory = currentDirectory.Parent;
        }

        solutionRoot = string.Empty;
        return false;
    }
}
