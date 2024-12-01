using System.Diagnostics;
namespace _2024.src.utils
{
    public static class DiagnosticUtils
    {
        public enum TimeUnit { Nanoseconds, Microseconds, Milliseconds, Ticks }

        public static long Benchmark(Action action, TimeUnit unit = TimeUnit.Ticks, int iterations = 1)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            for (int i = 0; i < iterations; ++i) action();

            long ticks = stopwatch.ElapsedTicks / iterations;

            return unit switch
            {
                TimeUnit.Microseconds => ticks / 10,
                TimeUnit.Milliseconds => ticks / 10000,
                TimeUnit.Nanoseconds => ticks * 100,
                _ => ticks,
            };
        }

        public static (long time, T result) Benchmark<T>(Func<T> func, TimeUnit unit = TimeUnit.Ticks, int iterations = 1)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            T result = default!;

            for (int i = 0; i < iterations; ++i) result = func();

            long ticks = stopwatch.ElapsedTicks / iterations;

            long time = unit switch
            {
                TimeUnit.Microseconds => ticks / 10,
                TimeUnit.Milliseconds => ticks / 10000,
                TimeUnit.Nanoseconds => ticks * 100,
                _ => ticks,
            };

            return (time, result);
        }
    };
}