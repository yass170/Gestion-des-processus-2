namespace ProcessusParent.Abstractions;

public interface IParentProcessResolver
{
    IReadOnlyDictionary<int, int> GetParentProcessIds();
}
