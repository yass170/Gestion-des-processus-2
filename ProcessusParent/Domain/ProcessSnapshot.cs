namespace ProcessusParent.Domain;

public sealed record ProcessSnapshot(
    int Id,
    string Name,
    int? ParentProcessId,
    string Priority,
    long? VirtualMemoryMb,
    long? WorkingSetMb);
