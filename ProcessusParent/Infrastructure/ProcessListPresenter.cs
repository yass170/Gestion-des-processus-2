using System.Globalization;
using ProcessusParent.Abstractions;

namespace ProcessusParent.Infrastructure;

public sealed class ProcessListPresenter : IProcessListPresenter
{
    private readonly IConsole _console;
    private readonly IProcessInspector _processInspector;

    public ProcessListPresenter(IConsole console, IProcessInspector processInspector)
    {
        _console = console;
        _processInspector = processInspector;
    }

    public void PrintRunningProcesses()
    {
        _console.WriteLine("Id     Name                             Priority     VirtualMB   WorkingMB");
        _console.WriteLine("------ -------------------------------- ------------ ---------- ----------");

        foreach (var process in _processInspector.GetRunningProcesses())
        {
            string virtualMb = process.VirtualMemoryMb?.ToString(CultureInfo.InvariantCulture) ?? "N/A";
            string workingMb = process.WorkingSetMb?.ToString(CultureInfo.InvariantCulture) ?? "N/A";

            _console.WriteLine(
                $"{process.Id,6} {process.Name,-32} {process.Priority,-12} {virtualMb,10} {workingMb,10}");
        }
    }
}
