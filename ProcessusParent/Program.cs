using System.Diagnostics;
using System.IO;

string? solutionRoot = FindSolutionRoot(AppContext.BaseDirectory, "Gestion-des-processus-2.sln");
if (solutionRoot is null)
{
    Console.Error.WriteLine("Solution root not found. Run from inside the repository.");
    Environment.ExitCode = 1;
    return;
}

string helloExe = Path.Combine(
    solutionRoot,
    "ConsoleHelloWorld",
    "bin",
    "Debug",
    "net10.0",
    "ConsoleHelloWorld.exe");

if (!File.Exists(helloExe))
{
    Console.Error.WriteLine($"Executable not found: {helloExe}");
    Console.Error.WriteLine("Build ConsoleHelloWorld first (dotnet build ConsoleHelloWorld).");
    Environment.ExitCode = 1;
    return;
}

Console.WriteLine("Lancement de ConsoleHelloWorld...");
Process? helloProcess = Process.Start(new ProcessStartInfo
{
    FileName = helloExe,
    Arguments = "Alice 3000",
    UseShellExecute = false
});

if (helloProcess is null)
{
    Console.Error.WriteLine("Impossible de lancer ConsoleHelloWorld.");
    Environment.ExitCode = 1;
    return;
}

Console.WriteLine("Processus principal continue après le lancement.");

Console.WriteLine("Lancement de explorer.exe...");
Process.Start(new ProcessStartInfo
{
    FileName = "explorer.exe",
    UseShellExecute = true
});

Console.WriteLine("Lancement de notepad.exe...");
Process.Start(new ProcessStartInfo
{
    FileName = "notepad.exe",
    UseShellExecute = true
});

Console.WriteLine("Fin du processus principal.");

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
