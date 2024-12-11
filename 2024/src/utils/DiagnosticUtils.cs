using System.Diagnostics;
namespace _2024.src.Utils
{
    public static class DiagnosticUtils
    {
        public enum TimeUnit { Nanoseconds, Microseconds, Milliseconds, Ticks }
        public static readonly long frequency = Stopwatch.Frequency;


        public static decimal Benchmark(Action action, TimeUnit unit = TimeUnit.Ticks, int iterations = 1)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            for (int i = 0; i < iterations; ++i) action();

            decimal ticks = stopwatch.ElapsedTicks / iterations;

            decimal time = unit switch
            {
                TimeUnit.Milliseconds => ticks * 1_000 / frequency,
                TimeUnit.Microseconds => ticks * 1_000_000 / frequency,
                TimeUnit.Nanoseconds => ticks * 1_000_000_000 / frequency,
                _ => ticks,
            };

            return Math.Round(time, 5);
        }

        public static (decimal time, T result) Benchmark<T>(Func<T> func, TimeUnit unit = TimeUnit.Ticks, int iterations = 1)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            T result = default!;

            for (int i = 0; i < iterations; ++i) result = func();

            decimal ticks = stopwatch.ElapsedTicks / iterations;

            decimal time = unit switch
            {
                TimeUnit.Microseconds => ticks / 10,
                TimeUnit.Milliseconds => ticks / 10000,
                TimeUnit.Nanoseconds => ticks * 100,
                _ => ticks,
            };

            return (Math.Round(time, 5), result);
        }
    };
}