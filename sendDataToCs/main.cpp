#include <windows.h>
#include <iostream>

using namespace std;

int main()
{
    const char *sharedMemoryName = "MyShareMemory";
    const int sharedMemorySize = sizeof(int);

    // 创建共享内存
    HANDLE hMapFile = CreateFileMappingA(
        INVALID_HANDLE_VALUE, // 使用无效的句柄作为文件句柄
        NULL,                 // 默认的安全性
        PAGE_READWRITE,       // 可读写的共享内存
        0,
        sharedMemorySize, // 共享内存大小
        sharedMemoryName);

    if (hMapFile == NULL)
    {
        cout << "Could not create file mapping object (" << GetLastError() << ")" << endl;
        return 1;
    }

    // 映射共享内存到当前进程的地址空间
    LPVOID pBuf = MapViewOfFile(
        hMapFile,
        FILE_MAP_ALL_ACCESS, // 可以完全访问共享内存
        0,
        0,
        sharedMemorySize);

    if (pBuf == NULL)
    {
        cout << "Could not map view of file (" << GetLastError() << ")" << endl;

        // 关闭共享内存句柄
        CloseHandle(hMapFile);
        return 1;
    }

    // 在共享内存中写入数据
    int value = 123;
    memcpy(pBuf, &value, sharedMemorySize);

    // 等待一段时间，以便C#程序可以读取共享内存中的数据
    Sleep(5000);

    // 解除映射
    UnmapViewOfFile(pBuf);

    // 关闭共享内存句柄
    CloseHandle(hMapFile);

    return 0;
}
