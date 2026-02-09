namespace ProcessusParent.Abstractions;

public interface IInteractiveMenu
{
    int PromptSelection(string title, IReadOnlyList<string> options);
}
