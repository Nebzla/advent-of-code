using System.Reflection;
using _2024.src.Interfaces;

namespace _2024.src.Utils
{
    public class SolutionUtils
    {
        private static void Solve(ushort day, ISolution instance)
        {
            string[] input = ParsingUtils.GetInput(day);
            string continuousInput = ParsingUtils.GetContinuousInput(day);

            Console.WriteLine($"Executing Day {day} Parts");
            Console.WriteLine("----------------------------------------------");
            double setupTime = DiagnosticUtils.Benchmark(() => instance.Setup(input, continuousInput), DiagnosticUtils.TimeUnit.Microseconds);
            Console.WriteLine($"Setup Time: {setupTime / 1000}ms");

            (double timeA, string? resultA) = DiagnosticUtils.Benchmark(instance.ExecPartA, DiagnosticUtils.TimeUnit.Microseconds);
            (double timeB, string? resultB) = DiagnosticUtils.Benchmark(instance.ExecPartB, DiagnosticUtils.TimeUnit.Microseconds);

            if(resultA == null && resultB == null) Console.WriteLine("Results returned null, skipping diagnostic");
            else
            {
                if(resultA != null) Console.WriteLine($"Part A: {resultA}, Took: {timeA / 1000}ms");
                if(resultB != null) Console.WriteLine($"Part B: {resultB}, Took: {timeB / 1000}ms");
                Console.WriteLine($"Total Execution Time: {(timeA + timeB + setupTime) / 1000}ms");
            }

            Console.WriteLine("----------------------------------------------");
        }

        public static void PrintAllSolutions()
        {
            List<ISolution> solutions = [.. Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as ISolution)
            .OfType<ISolution>()
            .OrderBy(i => i.DayNumber)];
            

            foreach(ISolution instance in solutions) 
            {
                Solve(instance.DayNumber, instance);
            }
        }

        public static void PrintSolution(ushort day)
        {
            if (day < 1 || day > 25) throw new ArgumentException("Invalid day entered");
            ISolution instance = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface)
            .Select(t => Activator.CreateInstance(t) as ISolution)
            .FirstOrDefault(s => s != null && s.DayNumber == day) ?? throw new ArgumentException($"Day {day} does not exist in program");

            Solve(day, instance);
        }

        
        public static void PrintSolutions(ushort[] days)
        {
            foreach(ushort day in days)
            {
                PrintSolution(day);
            }
        }
    }
}