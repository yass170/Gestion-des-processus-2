using ProcessusParent.Domain;

namespace ProcessusParent.Abstractions;

public interface IProcessInspector
{
    IReadOnlyCollection<ProcessSnapshot> GetRunningProcesses();
}
