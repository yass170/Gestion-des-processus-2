using System.Diagnostics;
using System.Linq;
using ProcessusParent.Abstractions;
using ProcessusParent.Domain;

namespace ProcessusParent.Infrastructure;

public sealed class ProcessInspector : IProcessInspector
{
    private readonly IParentProcessResolver _parentProcessResolver;

    public ProcessInspector(IParentProcessResolver parentProcessResolver)
    {
        _parentProcessResolver = parentProcessResolver;
    }

    public IReadOnlyCollection<ProcessSnapshot> GetRunningProcesses()
    {
        var snapshots = new List<ProcessSnapshot>();
        IReadOnlyDictionary<int, int> parentProcessIds = _parentProcessResolver.GetParentProcessIds();

        foreach (Process process in Process.GetProcesses().OrderBy(p => p.ProcessName, StringComparer.OrdinalIgnoreCase))
        {
            snapshots.Add(BuildSnapshot(process, parentProcessIds));
            process.Dispose();
        }

        return snapshots;
    }

    private static ProcessSnapshot BuildSnapshot(
        Process process,
        IReadOnlyDictionary<int, int> parentProcessIds)
    {
        int? parentId = parentProcessIds.TryGetValue(process.Id, out int foundParentId)
            ? foundParentId
            : null;

        return new ProcessSnapshot(
            process.Id,
            process.ProcessName,
            parentId,
            SafeGetPriority(process),
            SafeGetVirtualMemoryMb(process),
            SafeGetWorkingSetMb(process));
    }

    private static string SafeGetPriority(Process process)
    {
        try
        {
            return process.PriorityClass.ToString();
        }
        catch
        {
            return "N/A";
        }
    }

    private static long? SafeGetVirtualMemoryMb(Process process)
    {
        try
        {
            return process.VirtualMemorySize64 / (1024 * 1024);
        }
        catch
        {
            return null;
        }
    }

    private static long? SafeGetWorkingSetMb(Process process)
    {
        try
        {
            return process.WorkingSet64 / (1024 * 1024);
        }
        catch
        {
            return null;
        }
    }
}
