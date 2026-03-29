using ProcessusParent.Abstractions;

namespace ProcessusParent.Infrastructure;

public sealed class TextFileWriter : ITextFileWriter
{
    public void WriteAllText(string path, string content)
    {
        File.WriteAllText(path, content);
    }
}
