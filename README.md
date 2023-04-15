# CPP 和 C# 通过共享内存的方法实现通信

- sendDataToCs ：cpp 工程，创建共享内存空间，并存储数据
- getDataForCpp：c# 工程，读取共享内存空间中的数据

本项目在 Windows 环境中创建，目的是方便 Unity 调用

## Vscode 运行 C#

1. 查看是否安装成功 .NET 框架

```
dotnet
```

2. 安装 C# 扩展

3. 创建 C# 项目

```
dotnet new console --name Learncs
```

4. 进入项目文件夹，输入 `code .` 将会打开 vscode 并创建一个 `Hello World` 程序

5. 在终端中使用 `dotnet build` 进行编译

6. 运行 `dotnet run`

## CMake

在 Windows vscode 环境配合 MinGW 使用 cmake，具体查看 sendDataToCs 中 CMakeLists.txt 中的注释

```
cmake -G "MinGW Makefiles" ..
mingw32-make
```
