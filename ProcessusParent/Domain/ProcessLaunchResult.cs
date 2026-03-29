namespace ProcessusParent.Domain;

public sealed record ProcessLaunchResult(
    bool Success,
    string? ProcessName,
    int? ProcessId,
    string? ErrorMessage)
{
    public static ProcessLaunchResult FromSuccess(string processName, int processId)
    {
        return new ProcessLaunchResult(true, processName, processId, null);
    }

    public static ProcessLaunchResult FromFailure(string errorMessage)
    {
        return new ProcessLaunchResult(false, null, null, errorMessage);
    }
}
