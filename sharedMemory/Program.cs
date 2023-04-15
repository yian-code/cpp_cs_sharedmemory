using System;
using System.IO.MemoryMappedFiles;

class Program
{
    static void Main()
    {
        // 创建共享内存文件
        using (var mmf = MemoryMappedFile.CreateNew("ShareMemory", 1000))
        {
            // 在共享内存中创建一个视图
            using (var accessor = mmf.CreateViewAccessor())
            {
                // 写入数据到共享内存中
                accessor.Write(0, (int)42);
                accessor.Write(4, 3.14159);

                // 从共享内存中读取数据
                int intValue;
                double doubleValue;
                accessor.Read(0, out intValue);
                accessor.Read(4, out doubleValue);

                Console.WriteLine("Int value: {0}", intValue);
                Console.WriteLine("Double value: {0}", doubleValue);
            }
        }
    }
}
