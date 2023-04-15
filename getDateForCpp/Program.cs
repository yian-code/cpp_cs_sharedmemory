using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        const string sharedMemoryName = "MyShareMemory";
        const int sharedMemorySize = sizeof(int);

        // 打开共享内存
        IntPtr hMapFile = OpenFileMapping(
            FileMapAccess.Read, // 只读访问
            false,              // 不需要继承句柄
            sharedMemoryName);

        if (hMapFile == IntPtr.Zero)
        {
            Console.WriteLine("Could not open file mapping object (" + Marshal.GetLastWin32Error() + ")");
            return;
        }

        // 映射共享内存
        IntPtr pBuf = MapViewOfFile(
            hMapFile,           // 共享内存句柄
            FileMapAccess.Read, // 只读访问
            0,
            0,
            sharedMemorySize);

        if (pBuf == IntPtr.Zero)
        {
            Console.WriteLine("Could not map view of file (" + Marshal.GetLastWin32Error() + ")");

            // 关闭共享内存句柄
            CloseHandle(hMapFile);
            return;
        }

        // 读取共享内存中的数据
        int value = Marshal.ReadInt32(pBuf);
        Console.WriteLine("Value in shared memory: " + value);

        // 解除映射
        UnmapViewOfFile(pBuf);

        // 关闭共享内存句柄
        CloseHandle(hMapFile);

        Console.WriteLine("Press enter to exit...");
        Console.ReadLine();
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr OpenFileMapping(
        FileMapAccess dwDesiredAccess,
        bool bInheritHandle,
        string lpName);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr MapViewOfFile(
        IntPtr hFileMappingObject,
        FileMapAccess dwDesiredAccess,
        uint dwFileOffsetHigh,
        uint dwFileOffsetLow,
        uint dwNumberOfBytesToMap);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);

    [Flags]
    enum FileMapAccess : uint
    {
        Read = 0x0004,
        Write = 0x0002,
        ReadWrite = Read + Write,
        Copy = 0x0001,
        Execute = 0x0020,
    }
}
