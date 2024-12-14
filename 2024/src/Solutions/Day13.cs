using System.Numerics;
using _2024.src.Interfaces;
using _2024.src.Types;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public partial class Day13 : ISolution
    {
        public ushort DayNumber => 13;
        private Machine[] machines = [];
        const long PRIZE_MODIFIER = 10000000000000;

        private struct Machine(Vector2Int a, Vector2Int b, Vector2Int prize)
        {
            public Vector2Int AMovement = a;
            public Vector2Int BMovement = b;
            public Vector2Int Prize = prize;
            public Vector2Long LargePrize = new(prize.x + PRIZE_MODIFIER, prize.y + PRIZE_MODIFIER);
        }
        
        private struct LinearEquation(long x, long y, long constant)
        {
            public long x = x;
            public long y = y;
            public long constant = constant;

            public readonly LinearEquation Multiply(long n) => new(x * n, y * n, constant * n);

            public readonly (double x, double y)? SolveSimultaneous(LinearEquation other)
            {
                SquareMatrix mat = new(2, [x, y, other.x, other.y]); // Coefficient Matrix
                SquareMatrix matX = new(2, [constant, y, other.constant, other.y]); // First column replaced with constants
                SquareMatrix matY = new(2, [x, constant, other.x, other.constant]); // Second column replaced with constants
                
                if(mat.Determinant == 0) return null;
                return new(matX.Determinant / mat.Determinant, matY.Determinant / mat.Determinant); // Cramer method        
            }
        }

        private static (int a, int b) GetButtonPresses(Machine machine)
        {
            LinearEquation XEquation = new(machine.AMovement.x, machine.BMovement.x, machine.Prize.x);
            LinearEquation YEquation = new(machine.AMovement.y, machine.BMovement.y, machine.Prize.y);

            (double x, double y)? solution = XEquation.SolveSimultaneous(YEquation);

            // If no solutions or they are decimal (invalid), then return -1
            if(solution == null || Math.Floor(solution.Value.x) != solution.Value.x || Math.Floor(solution.Value.y) != solution.Value.y) return (-1, -1);
            return ((int) solution.Value.x, (int) solution.Value.y);
        }

        private static (long a, long b) GetLargeButtonPresses(Machine machine)
        {
            LinearEquation XEquation = new(machine.AMovement.x, machine.BMovement.x, machine.LargePrize.x);
            LinearEquation YEquation = new(machine.AMovement.y, machine.BMovement.y, machine.LargePrize.y);

            (double x, double y)? solution = XEquation.SolveSimultaneous(YEquation);

            // If no solutions or they are decimal (invalid), then return -1
            if(solution == null || Math.Floor(solution.Value.x) != solution.Value.x || Math.Floor(solution.Value.y) != solution.Value.y) return (-1, -1);
            return ((long) solution.Value.x, (long) solution.Value.y);
        }





        public string? ExecPartA() 
        {
            int total = 0;
            foreach(Machine machine in machines)
            {
                (int a, int b) = GetButtonPresses(machine);
                if(a == -1) continue;
                total += (a * 3) + b;
            }

            return total.ToString();
        }
        public string? ExecPartB()
        {
            BigInteger total = 0;
            foreach(Machine machine in machines)
            {
                (long a, long b) = GetLargeButtonPresses(machine);
                if(a == -1) continue;
                total += (a * 3) + b;
            }

            return total.ToString();  
        }

        public void Setup(string[] input, string continuousInput)
        {
            string[] machineStrings = continuousInput.Replace("\r\n", "\n").Split("\n\n"); // Compatable with both Windows and Linux
            machines = new Machine[machineStrings.Length];

            for(int i = 0; i < machines.Length; ++i)
            {
                string[] lines = machineStrings[i].Split('\n');
                if(lines.Length != 3) continue;

                int[] aCoords = ParsingUtils.ParseDigits(lines[0]).ToArray();
                int[] bCoords = ParsingUtils.ParseDigits(lines[1]).ToArray();
                int[] prizeCoords = ParsingUtils.ParseDigits(lines[2]).ToArray();

                if(aCoords.Length != 2 || bCoords.Length != 2 || prizeCoords.Length != 2) continue;

                machines[i] = new(new(aCoords[0], aCoords[1]), new(bCoords[0], bCoords[1]), new(prizeCoords[0], prizeCoords[1]));
            }  
        }

    }
}