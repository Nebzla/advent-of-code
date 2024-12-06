using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024.src
{
    public class Day6 : ISolution
    {
        public ushort DayNumber => 6;

        private char[][] grid = [];
        private int xLen;
        private int yLen;

        private (int x, int y) GetGuardPosition()
        {
            for (int y = 0; y < yLen; ++y)
            {
                int x = Array.IndexOf(grid[y], '^');
                if (x != -1) return (x, y);
            }

            return (-1, -1);
        }

        readonly (int xDir, int yDir)[] directions = [(0, -1), (1, 0), (0, 1), (-1, 0)];
        private HashSet<(int, int)> GetGuardPath(int x, int y)
        {
            int dirIndex = 0;

            HashSet<(int, int)> visited = [(x, y)];

            // While the guard hasn't escaped the grid, continue along path
            while (true)
            {
                (int currentXDir, int currentYDir) = directions[dirIndex];
                int newX = x + currentXDir;
                int newY = y + currentYDir;

                visited.Add((x, y));
                if (newX == xLen || newX < 0 || newY == yLen || newY < 0) break;


                if (grid[newY][newX] == '#') // If next spot is obstacle, rotate 90deg
                {
                    ++dirIndex;
                    if (dirIndex > 3) dirIndex = 0;
                    continue;
                }

                x = newX;
                y = newY;
            }

            return visited;
        }

        private bool DoesPathLoop(char[][] grid, int x, int y)
        {
            HashSet<(int, int)> visited = [];

            (int, int)? loopStart = null;

            int dirIndex = 0;

            // While the guard hasn't escaped the grid, continue along path
            while (true)
            {
                (int currentXDir, int currentYDir) = directions[dirIndex];
                int newX = x + currentXDir;
                int newY = y + currentYDir;

                if (newX == xLen || newX < 0 || newY == yLen || newY < 0) break;

                if (grid[newY][newX] == '#') // If next spot is obstacle, rotate 90deg
                {
                    ++dirIndex;
                    if (dirIndex > 3) dirIndex = 0;
                    continue;
                }

                x = newX;
                y = newY;
                if (!visited.Add((x, y)))
                {
                    // If visited the same position for a second time
                    if (loopStart == null) loopStart = (x, y);
                    else if ((x, y) == loopStart) return true; // If looped back on itself, a permanent loop has formed
                }
                else loopStart = null;

            }
            return false;
        }


        public string? ExecPartA()
        {
            (int xG, int yG) = GetGuardPosition();
            return GetGuardPath(xG, yG).Count.ToString();
        }

        public string? ExecPartB()
        {
            int total = 0;
            (int xG, int yG) = GetGuardPosition();
            HashSet<(int, int)> guardPath = GetGuardPath(xG, yG);
            foreach ((int x, int y) in guardPath)
            {
                if (x == xG && y == yG) continue;

                char[][] newGrid = GeneralUtils.DeepCopy2DArray(grid);
                newGrid[y][x] = '#';
                if (DoesPathLoop(newGrid, xG, yG)) ++total;
            }
            return total.ToString();
        }

        public void Setup(string[] input, string continuousInput)
        {
            if (input.Length == 0 || input[0].Length == 0) return;

            grid = input.Select(r => r.ToCharArray()).ToArray();
            xLen = input[0].Length;
            yLen = input.Length;
        }

    }
}