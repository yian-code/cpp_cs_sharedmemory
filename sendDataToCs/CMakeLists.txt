cmake_minimum_required(VERSION 3.10)
# 设置C编译器
set(CMAKE_C_COMPILER "gcc")
# 设置C++编译器
set(CMAKE_CXX_COMPILER "g++")

project(sendDataToCs)

# 指定生成目标文件夹
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "./bin") 

# 指定生成目标
add_executable(out main.cpp)
# cd build
# cmake -G "MinGW Makefiles" ..
# mingw32-make