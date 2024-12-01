using System.Reflection;
using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024
{
    internal class Program
    {
        static void Solve(ushort day, ISolution instance)
        {
            string[] input = ParsingUtils.GetInput(day);

            Console.WriteLine($"Executing Day {day} Parts");
            Console.WriteLine("----------------------------------------------");
            long setupTime = DiagnosticUtils.Benchmark(() => instance.Setup(input), DiagnosticUtils.TimeUnit.Milliseconds);
            Console.WriteLine($"Setup Time: {setupTime}ms");

            (long timeA, string resultA) = DiagnosticUtils.Benchmark(instance.ExecPartA, DiagnosticUtils.TimeUnit.Milliseconds);
            (long timeB, string resultB) = DiagnosticUtils.Benchmark(instance.ExecPartB, DiagnosticUtils.TimeUnit.Milliseconds);
            Console.WriteLine($"Part A: {resultA}, Took: {timeA}ms");
            Console.WriteLine($"Part B: {resultB}, Took: {timeB}ms");

            Console.WriteLine($"Total Execution Time: {timeA + timeB + setupTime}ms");
            Console.WriteLine("----------------------------------------------");
        }

        static void PrintAllSolutions()
        {
            List<ISolution> solutions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as ISolution)
            .OfType<ISolution>()
            .ToList();

            foreach(ISolution instance in solutions) 
            {
                Solve(instance.DayNumber, instance);
            }
        }

        static void PrintSolution(ushort day)
        {
            ISolution instance = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as ISolution)
            .FirstOrDefault(s => s != null && s.DayNumber == day) ?? throw new ArgumentException($"Day {day} does not exist in program");

            Solve(day, instance);
        }

        static void Main()
        {
            PrintAllSolutions();
        }
    }
}