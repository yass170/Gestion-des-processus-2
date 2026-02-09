namespace ProcessusParent.Abstractions;

public interface ISolutionLocator
{
    bool TryFindSolutionRoot(string startDirectory, string solutionFileName, out string solutionRoot);
}
