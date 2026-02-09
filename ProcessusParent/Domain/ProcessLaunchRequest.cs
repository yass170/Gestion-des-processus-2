namespace ProcessusParent.Domain;

public sealed record ProcessLaunchRequest(
    string FileName,
    string? Arguments,
    bool UseShellExecute,
    string? Verb,
    bool WaitForExit,
    int ParentDelayMilliseconds);
