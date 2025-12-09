#include <algorithm>
#include <chrono>
#include <cstdint>
#include <sys/types.h>
#include <type_traits>
#include <variant>
#include <vector>

using namespace std::chrono;

template <typename T>
struct BenchmarkResult {
    T returnValue;

    std::vector<uint64_t> times;

    uint64_t total {};
    uint64_t average {};

    uint64_t slowest = 0;
    uint64_t quickest = UINT64_MAX;


    explicit BenchmarkResult(std::vector<uint64_t> times, T returnValue) {

        for(uint64_t time : times) {
            total += time;
            slowest = std::max(time, slowest);
            quickest = std::min(time, quickest);
        }

        average = total / times.size();

        this->times = std::move(times);
        this->returnValue = returnValue;
    }
};

template <typename Func, typename... Args>
auto benchmark(Func&& func, Args&&... args, uint iterations = 1) {
    // Account for void and make return type monostate if so
    using raw_return_t = std::invoke_result_t<Func&&, Args&&...>;
    using return_t = std::conditional_t<std::is_void_v<raw_return_t>, std::monostate, raw_return_t>;

    return_t returnValue {};
    std::vector<uint64_t> times;
    times.reserve(iterations);

    for(uint i = 0; i < iterations; i++) {
        auto start = high_resolution_clock::now();

        // constexpr avoids compliling else branch if type returned is void to prevent compilation error when assigning void to returnValue
        if constexpr (std::is_void_v<raw_return_t>) func(args...); 
        else returnValue = func(args...);

        auto end = high_resolution_clock::now();

        auto duration = duration_cast<nanoseconds>(end - start).count();
        times.push_back(duration);
    }

    return BenchmarkResult<return_t>{times, returnValue};
}