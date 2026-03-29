using ConsoleHelloWorld.Abstractions;
using ConsoleHelloWorld.Application;
using ConsoleHelloWorld.Infrastructure;

namespace ConsoleHelloWorld;

public static class Program
{
    public static int Main(string[] args)
    {
        IConsole console = new SystemConsole();
        IGreetingService greetingService = new GreetingService();
        IExecutionDelay executionDelay = new ThreadExecutionDelay();

        var application = new HelloWorldApplication(console, greetingService, executionDelay);
        return application.Run(args);
    }
}
