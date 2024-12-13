using _2024.src.Interfaces;
using _2024.src.Types;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day13 : ISolution
    {
        public ushort DayNumber => 13;
        private Machine[] machines = [];


        private struct Machine(Vector2Int a, Vector2Int b, Vector2Int prize)
        {
            public Vector2Int AMovement = a;
            public Vector2Int BMovement = b;
            public Vector2Int Prize = prize;
        }

        public void Setup(string[] input, string continuousInput)
        {
            string[] machineStrings = continuousInput.Split("\n\n");
            machines = new Machine[machineStrings.Length];

            for(int i = 0; i < machines.Length; ++i)
            {
                string[] lines = machineStrings[i].Split("\n");
                if(lines.Length != 3) continue;

                int[] aCoords = ParsingUtils.ParseDigits(lines[0]).ToArray();
                int[] bCoords = ParsingUtils.ParseDigits(lines[1]).ToArray();
                int[] prizeCoords = ParsingUtils.ParseDigits(lines[2]).ToArray();

                if(aCoords.Length != 2 || bCoords.Length != 2 || prizeCoords.Length != 2) continue;

                machines[i] = new(new(aCoords[0], aCoords[1]), new(bCoords[0], bCoords[1]), new(prizeCoords[0], prizeCoords[1]));
            }  
        }

        private static (int a, int b) GetBruteForceButtonPresses(Machine machine)
        {
            // Get every combinatiton of a and b movements to check if can reach prize
            Vector2Int aMovement;
            Vector2Int bMovement;
            for(int a = 0; true; ++a)
            {
                for(int b = 0; true; ++b)
                {
                    aMovement = machine.AMovement * a;
                    bMovement = machine.BMovement * b;
                    Vector2Int movement = aMovement + bMovement;

                    if(movement.x > machine.Prize.x || movement.y > machine.Prize.y) break;
                    else if(movement == machine.Prize) return (a, b);
                }

                if(aMovement.x > machine.Prize.x || aMovement.y > machine.Prize.y) return (-1, -1);
            }
        }


        private struct LinearEquation(int x, int y, int constant)
        {
            public int x = x;
            public int y = y;
            public int constant = constant;

            public readonly LinearEquation Multiply(int n) => new(x * n, y * n, constant * n);

            public readonly (int x, int y) SolveSimultaneous(LinearEquation other)
            {
                int[,] coefficientMatrix = new int[2, 2]
                {
                    {x, y},
                    {other.x, other.y}
                };
                int[] constantMatrix = [constant, other.constant];
                int coefficientDet = Matrix2x2.GetDeterminant(coefficientMatrix);

                return (0, 0);
            }
        }

        private static (int a, int b) GetSimultaneousButtonPresses(Machine machine)
        {
            LinearEquation XEquation = new(machine.AMovement.x, machine.BMovement.x, machine.Prize.x);
            LinearEquation YEquation = new(machine.AMovement.y, machine.BMovement.y, machine.Prize.y);

            (int x, int y) = XEquation.SolveSimultaneous(YEquation);

            if(x % 1 != 0 || y % 1 != 0) return (-1, -1);
            return (x, y);
        }




        public string? ExecPartA() 
        {
            long total = 0;
            foreach(Machine machine in machines)
            {
                (int a, int b) = GetSimultaneousButtonPresses(machine);
                if(a == -1) continue;
                total += (a * 3) + b;
            }

            return total.ToString();
        }
        public string? ExecPartB()
        {
            long total = 0;
            foreach(Machine machine in machines)
            {
                (int a, int b) = GetBruteForceButtonPresses(machine);
                if(a == -1) continue;
                total += (a * 3) + b;
            }

            return total.ToString();
        }



    }
}