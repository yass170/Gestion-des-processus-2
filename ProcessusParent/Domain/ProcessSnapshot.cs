namespace ProcessusParent.Domain;

public sealed record ProcessSnapshot(
    int Id,
    string Name,
    string Priority,
    long? VirtualMemoryMb,
    long? WorkingSetMb);
