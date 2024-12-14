using _2024.src.Interfaces;
using _2024.src.Types;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day12 : ISolution
    {
        public ushort DayNumber => 12;
        private char[,] grid = new char[0, 0];
        private Region[] regions = [];
        private int xLen;
        private int yLen;

        private class Region(char id)
        {
            public readonly char identifier = id;
            public HashSet<Vector2Int> perimeterNodes = [];
            public HashSet<Vector2Int> regionNodes = [];

            public int perimeter = 0;

            public int Sides => GetSides(perimeterNodes);
            public int Area => regionNodes.Count;
            public int Price => perimeter * Area;
            public int SidePrice => Sides * Area;

            private static Vector2Int GetRelativeLeftDirection(Vector2Int currentDirection)
            {
                int index = Array.IndexOf(Vector2Int.Directions, currentDirection);
                if (index == -1) throw new ArgumentException("Invalid direction");

                int newIndex = (index - 1 + Vector2Int.Directions.Length) % Vector2Int.Directions.Length;
                return Vector2Int.Directions[newIndex];
            }

            private static Vector2Int GetRelativeRightDirection(Vector2Int currentDirection)
            {
                int index = Array.IndexOf(Vector2Int.Directions, currentDirection);
                if (index == -1) throw new ArgumentException("Invalid direction");

                int newIndex = (index + 1) % Vector2Int.Directions.Length;
                return Vector2Int.Directions[newIndex];
            }

            private Vector2Int GetInitialDirection(Vector2Int pos, HashSet<Vector2Int> nodes)
            {
                foreach (Vector2Int direction in Vector2Int.Directions)
                {
                    if (nodes.Contains(pos + direction) && regionNodes.Contains(pos + GetRelativeLeftDirection(direction))) return direction;
                }

                return Vector2Int.Zero;
            }

            private int GetSides(HashSet<Vector2Int> nodes)
            {
                if (nodes.Count == 1) return 4; // If in isolation, it is automatically known to have 4 corners

                int corners = 0;

                Vector2Int start = nodes.First(); // Get random starting wall node
                Vector2Int startDirection = GetInitialDirection(start, nodes);
                Vector2Int currentWallPos = start;
                Vector2Int currentDirection = startDirection;

                for (int i = 0; startDirection == Vector2Int.Zero; ++i) // If node selected is in isolation, pick another one
                {
                    if(i >= nodes.Count) return 4 * nodes.Count;
                    currentWallPos = nodes.ToList()[i];
                    startDirection = GetInitialDirection(currentWallPos, nodes);
                    currentDirection = startDirection;
                }

                HashSet<Vector2Int> visited = [currentWallPos];

                // Trace around wall as if it was a maze, hugging the left wall at all times unless it loops back on itself

                do
                {
                    Vector2Int left = GetRelativeLeftDirection(currentDirection);
                    if (!regionNodes.Contains(currentWallPos + left)) // If no inside wall to the left
                    {
                        // Turn to the left, and move forward as wall should follow along that path
                        currentDirection = left;
                        currentWallPos += left;
                        corners++;

                    }
                    else if (!regionNodes.Contains(currentWallPos + currentDirection)) // If no inside wall ahead, keep going (no corner)
                    {
                        currentWallPos += currentDirection;

                    }
                    else // Otherwise a backtrack is going to happen
                    {
                        // Turn right until on straight path again
                        currentDirection = GetRelativeRightDirection(currentDirection);
                        corners++;
                    }

                    visited.Add(currentWallPos);

                    //! Need to handle case here where it completes a loop at a branch
                } while (currentWallPos != start || currentDirection != startDirection);

                if (visited.Count < nodes.Count) // If any isolated walls are left over, will need to traverse them
                {
                    HashSet<Vector2Int> unvisitedNodes = new(nodes);
                    unvisitedNodes.SymmetricExceptWith(visited);
                    corners += GetSides(unvisitedNodes);
                }

                return corners;
            }
        }



        private void FindRegionSize(Region region, Vector2Int pos, HashSet<Vector2Int> explored)
        {
            explored.Add(pos);
            region.regionNodes.Add(pos);
            foreach (Vector2Int direction in Vector2Int.DiagonalDirections)
            {
                Vector2Int newPos = pos + direction;

                if (explored.Contains(newPos)) continue; // If already explored ignore            


                // If out of region add permiter and ignore
                if (GridUtils.IsOutOfRange(newPos, xLen, yLen) || grid[newPos.x, newPos.y] != region.identifier)
                {

                    region.perimeterNodes.Add(newPos);
                    if (!direction.IsDiagonal()) region.perimeter++;
                    continue;
                }

                // Else in same region still then add to area and explore around that node
                if(!direction.IsDiagonal()) // Ensures it not diagonal as could join 2 regions adjacent through a diagonal
                {
                    region.regionNodes.Add(newPos);
                    FindRegionSize(region, newPos, explored);
                }
            }
        }

        private Region[] GetRegions()
        {
            HashSet<Region> regions = [];
            HashSet<Vector2Int> exploredCoords = [];

            for (int x = 0; x < xLen; ++x)
            {
                for (int y = 0; y < yLen; ++y)
                {
                    Vector2Int position = new(x, y);
                    if (exploredCoords.Contains(position)) continue;

                    Region region = new(grid[x, y]);

                    HashSet<Vector2Int> regionExplored = [];
                    FindRegionSize(region, position, regionExplored);

                    exploredCoords.UnionWith(regionExplored);
                    regions.Add(region);
                }
            }

            return [.. regions];
        }

        public string? ExecPartA()
        {
            int total = 0;
            foreach (Region region in regions)
            {
                total += region.Price;
            }
            return total.ToString();
        }

        public string? ExecPartB()
        {
            int total = 0;
            int i = 0;
            foreach (Region region in regions)
            {
                Console.WriteLine($"{i} / {regions.Length} Complete");
                total += region.SidePrice;
                ++i;
            }
            return total.ToString();
        }
        public void Setup(string[] input, string continuousInput)
        {
            grid = GridUtils.ConvertInputToGrid(input);
            xLen = grid.GetLength(0);
            yLen = grid.GetLength(1);
            regions = GetRegions();
        }
    }
}