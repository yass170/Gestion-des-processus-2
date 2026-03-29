using System.Runtime.InteropServices;
using ProcessusParent.Abstractions;

namespace ProcessusParent.Infrastructure;

public sealed class WindowsParentProcessResolver : IParentProcessResolver
{
    private const uint Th32csSnapprocess = 0x00000002;
    private static readonly IntPtr InvalidHandleValue = new(-1);

    public IReadOnlyDictionary<int, int> GetParentProcessIds()
    {
        var result = new Dictionary<int, int>();
        IntPtr snapshotHandle = CreateToolhelp32Snapshot(Th32csSnapprocess, 0);
        if (snapshotHandle == IntPtr.Zero || snapshotHandle == InvalidHandleValue)
        {
            return result;
        }

        try
        {
            var entry = new ProcessEntry32
            {
                DwSize = (uint)Marshal.SizeOf<ProcessEntry32>()
            };

            if (!Process32First(snapshotHandle, ref entry))
            {
                return result;
            }

            do
            {
                result[(int)entry.Th32ProcessId] = (int)entry.Th32ParentProcessId;
                entry.DwSize = (uint)Marshal.SizeOf<ProcessEntry32>();
            }
            while (Process32Next(snapshotHandle, ref entry));

            return result;
        }
        finally
        {
            CloseHandle(snapshotHandle);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct ProcessEntry32
    {
        public uint DwSize;
        public uint CntUsage;
        public uint Th32ProcessId;
        public IntPtr Th32DefaultHeapId;
        public uint Th32ModuleId;
        public uint CntThreads;
        public uint Th32ParentProcessId;
        public int PcPriClassBase;
        public uint DwFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string SzExeFile;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool Process32First(IntPtr hSnapshot, ref ProcessEntry32 lppe);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool Process32Next(IntPtr hSnapshot, ref ProcessEntry32 lppe);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);
}
