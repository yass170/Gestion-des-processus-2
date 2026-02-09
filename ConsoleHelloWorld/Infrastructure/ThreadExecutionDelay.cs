using System.Threading;
using ConsoleHelloWorld.Abstractions;

namespace ConsoleHelloWorld.Infrastructure;

public sealed class ThreadExecutionDelay : IExecutionDelay
{
    public void Wait(int delayMilliseconds)
    {
        Thread.Sleep(delayMilliseconds);
    }
}
