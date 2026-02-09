namespace ProcessusParent.Domain;

public sealed record LaunchMenuOption(
    string Label,
    ProcessLaunchRequest? Request,
    bool IsExitOption);
