using ProcessusParent.Domain;

namespace ProcessusParent.Abstractions;

public interface IProcessLauncher
{
    ProcessLaunchResult Launch(ProcessLaunchRequest request);
}
