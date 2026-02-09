using ProcessusParent.Domain;

namespace ProcessusParent.Abstractions;

public interface IProjectContextProvider
{
    bool TryGetProjectContext(out ProjectContext? context, out string errorMessage);
}
