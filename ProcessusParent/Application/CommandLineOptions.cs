namespace ProcessusParent.Application;

public sealed class CommandLineOptions
{
    private CommandLineOptions(ApplicationMode mode, bool isValid, string? validationMessage)
    {
        Mode = mode;
        IsValid = isValid;
        ValidationMessage = validationMessage;
    }

    public ApplicationMode Mode { get; }

    public bool IsValid { get; }

    public string? ValidationMessage { get; }

    public static CommandLineOptions CreateValid(ApplicationMode mode)
    {
        return new CommandLineOptions(mode, true, null);
    }

    public static CommandLineOptions CreateInvalid(string validationMessage)
    {
        return new CommandLineOptions(ApplicationMode.Demo, false, validationMessage);
    }
}
