using System.Diagnostics;
using System.IO;
using System.Threading;

// Locate the solution root so we can find the built ConsoleHelloWorld executable.
string? solutionRoot = FindSolutionRoot(AppContext.BaseDirectory, "Gestion-des-processus-2.sln");
if (solutionRoot is null)
{
    Console.Error.WriteLine("Solution root not found. Run from inside the repository.");
    Environment.ExitCode = 1;
    return;
}

// Build the path to the ConsoleHelloWorld executable (Debug build).
string helloExe = Path.Combine(
    solutionRoot,
    "ConsoleHelloWorld",
    "bin",
    "Debug",
    "net10.0",
    "ConsoleHelloWorld.exe");

// Ensure the child executable exists before starting it.
if (!File.Exists(helloExe))
{
    Console.Error.WriteLine($"Executable not found: {helloExe}");
    Console.Error.WriteLine("Build ConsoleHelloWorld first (dotnet build ConsoleHelloWorld).");
    Environment.ExitCode = 1;
    return;
}

// Start ConsoleHelloWorld as a child process.
Process? helloProcess = LaunchProcessWithMessage(new ProcessStartInfo
{
    FileName = helloExe,
    Arguments = "Alice 3000",
    UseShellExecute = false
});

if (helloProcess is null)
{
    Environment.ExitCode = 1;
    return;
}

Console.WriteLine("Processus principal continue apres le lancement.");

// Open Windows Explorer directly in C:\\Windows and wait for it to close.
LaunchAndWait(new ProcessStartInfo
{
    FileName = "explorer.exe",
    Arguments = "C:\\Windows",
    UseShellExecute = true
}, 2000);

// Create a text file and open it in Notepad.
string noteFile = Path.Combine(solutionRoot, "note-q5.txt");
File.WriteAllText(noteFile, "Fichier de test pour Q5.");

LaunchAndWait(new ProcessStartInfo
{
    FileName = "notepad.exe",
    Arguments = $"\"{noteFile}\"",
    UseShellExecute = true
}, 2000);

// Use shell verbs to open Explorer and win.ini in the default editor.
LaunchAndWait(new ProcessStartInfo
{
    FileName = "C:\\Windows",
    Verb = "explore",
    UseShellExecute = true
}, 2000);

LaunchAndWait(new ProcessStartInfo
{
    FileName = "C:\\Windows\\win.ini",
    Verb = "open",
    UseShellExecute = true
}, 2000);

Console.WriteLine("Fin du processus principal.");

/// <summary>
/// Walks up the directory tree to find the solution root.
/// </summary>
static string? FindSolutionRoot(string startDir, string solutionFileName)
{
    DirectoryInfo? dir = new DirectoryInfo(startDir);
    while (dir is not null)
    {
        string candidate = Path.Combine(dir.FullName, solutionFileName);
        if (File.Exists(candidate))
        {
            return dir.FullName;
        }

        dir = dir.Parent;
    }

    return null;
}

/// <summary>
/// Starts a process and prints its name and id to the console.
/// </summary>
static Process? LaunchProcessWithMessage(ProcessStartInfo startInfo)
{
    Process? process = Process.Start(startInfo);
    if (process is null)
    {
        Console.Error.WriteLine($"Impossible de lancer {startInfo.FileName}.");
        return null;
    }

    Console.WriteLine($"Processus {process.ProcessName} no {process.Id} est lance.");
    return process;
}

/// <summary>
/// Starts a process, waits for it to exit, and optionally sleeps the parent.
/// </summary>
static void LaunchAndWait(ProcessStartInfo startInfo, int parentDelayMs)
{
    Process? process = Process.Start(startInfo);
    if (process is null)
    {
        Console.Error.WriteLine($"Impossible de lancer {startInfo.FileName}.");
        return;
    }

    Console.WriteLine($"Processus {process.ProcessName} no {process.Id} est lance.");
    process.WaitForExit();
    Console.WriteLine($"Processus {process.ProcessName} no {process.Id} est termine.");

    if (parentDelayMs > 0)
    {
        Thread.Sleep(parentDelayMs);
    }
}
