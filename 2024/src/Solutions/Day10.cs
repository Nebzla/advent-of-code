using _2024.src.Interfaces;
using _2024.src.Utils;
using _2024.src.Types;

namespace _2024.src.Solutions
{
    public class Day10 : ISolution
    {
        public ushort DayNumber => 10;

        private int[,] grid = new int[0, 0];
        private Vector2Int[] startingPositions = [];
        private int xLen;
        private int yLen;

        private static readonly Vector2Int[] directions = [new(0, -1), new(1, 0), new(0, 1), new(-1, 0)];
        private void TraversePath(Vector2Int currentPosition, ref HashSet<Vector2Int> visitedPeaks, ref int pathTotal)
        {
            int currentHeight = grid[currentPosition.x, currentPosition.y];

            // Check each direction to see valid hiking trail
            foreach(Vector2Int direction in directions)
            {
                Vector2Int newPos = direction + currentPosition;
                if(newPos.x >= xLen || newPos.x < 0 || newPos.y >= yLen || newPos.y < 0) continue; // If direction is out of bounds
                if(grid[newPos.x, newPos.y] != currentHeight + 1) continue; // If not correct incline, is invalid

                if(currentHeight + 1 == 9)
                {
                    if(visitedPeaks.Contains(newPos)) continue; // If already exists in current hiking trail, ignore

                    ++ pathTotal; // If reached end of hiking trail, add to valid path
                    visitedPeaks.Add(newPos); // Add 9 value to visited peaks so it cant visit it again from same starting point

                } else TraversePath(newPos, ref visitedPeaks, ref pathTotal); // Otherwise add path as a new branch
            }
        }

        private void TraversePath(Vector2Int currentPosition, ref int pathTotal)
        {
            int currentHeight = grid[currentPosition.x, currentPosition.y];

            // Check each direction to see valid hiking trail
            foreach(Vector2Int direction in directions)
            {
                Vector2Int newPos = direction + currentPosition;
                if(newPos.x >= xLen || newPos.x < 0 || newPos.y >= yLen || newPos.y < 0) continue; // If direction is out of bounds
                if(grid[newPos.x, newPos.y] != currentHeight + 1) continue; // If not correct incline, is invalid

                if(currentHeight + 1 == 9) ++ pathTotal; // If reached end of hiking trail, add to valid path
                else TraversePath(newPos, ref pathTotal); // Otherwise add path as a new branch
            }
        }


        private int GetTotalUniqueTrails()
        {
            int total = 0;
            foreach(Vector2Int startPos in startingPositions)
            {
                HashSet<Vector2Int> visitedPeaks = [];
                TraversePath(startPos, ref visitedPeaks, ref total);
            }
            return total;
        }

        private int GetTotalTrails()
        {
            int total = 0;
            foreach(Vector2Int startPos in startingPositions)
            {
                TraversePath(startPos, ref total);
            }
            return total;
        }


        public string? ExecPartA() => GetTotalUniqueTrails().ToString();
        public string? ExecPartB() => GetTotalTrails().ToString();


        private Vector2Int[] GetStartingPositions()
        {
            List<Vector2Int> positions = [];

            for(int x = 0; x < xLen; ++x)
            {
                for(int y = 0; y < yLen; ++y)
                {
                    if(grid[x, y] == 0) positions.Add(new(x, y));
                }
            }

            return [.. positions];
        }

        public void Setup(string[] input, string continuousInput) 
        {
            grid = GridUtils.ConvertIntInputToGrid(input);
            xLen = grid.GetLength(0);
            yLen = grid.GetLength(1);

            startingPositions = GetStartingPositions();
        }
    }
}