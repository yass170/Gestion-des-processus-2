namespace ProcessusParent.Abstractions;

public interface ITextFileWriter
{
    void WriteAllText(string path, string content);
}
