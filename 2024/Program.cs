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
            Console.WriteLine('\n');
            List<Type> solutionTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface)
            .ToList();

            foreach (Type type in solutionTypes)
            {
                ISolution instance = Activator.CreateInstance(type) as ISolution
                ?? throw new InvalidOperationException($"Unable to create instance of {type.FullName}");

                PropertyInfo dayProperty = type.GetProperty("DayNumber") ?? throw new NullReferenceException("DayNumber doesn't exist as property");

                object dayVal = dayProperty.GetValue(null) ?? throw new NullReferenceException("DayNumber unexpectedly null");
                if (dayVal is not ushort day) throw new InvalidCastException("DayNumber is not of expected type ushort");

                Solve(day, instance);
            }
            Console.WriteLine('\n');
        }

        //TODO Needs Optimisation
        static void PrintSolution(ushort day)
        {
            Console.WriteLine('\n');
            List<Type> solutionTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface)
            .ToList();

            foreach (Type type in solutionTypes)
            {
                ISolution instance = Activator.CreateInstance(type) as ISolution
                ?? throw new InvalidOperationException($"Unable to create instance of {type.FullName}");

                PropertyInfo dayProperty = type.GetProperty("DayNumber") ?? throw new NullReferenceException("DayNumber doesn't exist as property");

                object dayVal = dayProperty.GetValue(null) ?? throw new NullReferenceException("DayNumber unexpectedly null");
                if (dayVal is not ushort dayNum) throw new InvalidCastException("DayNumber is not of expected type ushort");

                if (dayNum != day) continue;

                Solve(day, instance);
                break;
            }
            Console.WriteLine('\n');
        }

        static void Main()
        {
            PrintAllSolutions();
        }
    }
}