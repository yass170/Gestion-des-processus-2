using ConsoleHelloWorld.Abstractions;

namespace ConsoleHelloWorld.Infrastructure;

public sealed class GreetingService : IGreetingService
{
    public string BuildGreeting(string name)
    {
        return $"Hello {name}";
    }
}
