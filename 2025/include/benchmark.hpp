#include <chrono>
#include <iostream>
#include <ratio>

template <typename Func>
auto benchmark(Func func, int execIterations = 1) {
    auto start = std::chrono::high_resolution_clock::now();
    Func returnValue;

    for(int i = 0; i < execIterations; ++i) {
        returnValue = func();
    }

    auto end = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration<double, std::micro>(end - start).count();


    std::cout << "Average execution time: " << duration * 1000 << "ms";
    return returnValue;
}