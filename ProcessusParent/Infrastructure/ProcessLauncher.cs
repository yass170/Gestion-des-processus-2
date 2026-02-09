using System.Diagnostics;
using System.Threading;
using ProcessusParent.Abstractions;
using ProcessusParent.Domain;

namespace ProcessusParent.Infrastructure;

public sealed class ProcessLauncher : IProcessLauncher
{
    private readonly IConsole _console;

    public ProcessLauncher(IConsole console)
    {
        _console = console;
    }

    public ProcessLaunchResult Launch(ProcessLaunchRequest request)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = request.FileName,
            UseShellExecute = request.UseShellExecute
        };

        if (!string.IsNullOrWhiteSpace(request.Arguments))
        {
            startInfo.Arguments = request.Arguments;
        }

        if (!string.IsNullOrWhiteSpace(request.Verb))
        {
            startInfo.Verb = request.Verb;
        }

        try
        {
            using Process? process = Process.Start(startInfo);
            if (process is null)
            {
                return ProcessLaunchResult.FromFailure($"Unable to launch {request.FileName}.");
            }

            int parentProcessId = Process.GetCurrentProcess().Id;
            _console.WriteLine(
                $"Process {process.ProcessName} (Id: {process.Id}, ParentId: {parentProcessId}) started.");

            if (request.WaitForExit)
            {
                process.WaitForExit();
                _console.WriteLine(
                    $"Process {process.ProcessName} (Id: {process.Id}, ParentId: {parentProcessId}) exited.");

                if (request.ParentDelayMilliseconds > 0)
                {
                    Thread.Sleep(request.ParentDelayMilliseconds);
                }
            }

            return ProcessLaunchResult.FromSuccess(process.ProcessName, process.Id);
        }
        catch (Exception ex)
        {
            return ProcessLaunchResult.FromFailure($"Failed to launch {request.FileName}: {ex.Message}");
        }
    }
}
