using _2024.src.Interfaces;
using _2024.src.Types;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day12 : ISolution
    {
        public ushort DayNumber => 12;
        private char[,] grid = new char[0, 0];
        private int xLen;
        private int yLen;

        private class Region(char id)
        {
            public readonly char identifier = id;

            public int area = 1;
            public int perimeter = 0;
            public HashSet<HashSet<Vector2Int>> sides = [];

            public int Price => area * perimeter;
        }

        // private class SideRegion(char id)
        // {
        //     public readonly char identifier = id;

        //     public int area = 1;

        //     public HashSet<Vector2Int> perimeterCoords = [];

        //     public int Sides => GetSides();
        //     public int Price => area * Sides;
        //     public int Perimeter => perimeterCoords.Count;


        //     private static readonly Vector2Int[] hDirections = [new(1, 0), new(-1, 0)];
        //     private static readonly Vector2Int[] vDirections = [new(0, 1), new(0, -1)];
        //     private int GetSides()
        //     {
        //         int total = 0;


        //         foreach(Vector2Int perimiterStart in perimeterCoords)
        //         {
        //             if()
        //         }

        //         // If it is on a corner between 2 or more region nodes, it needs to that many nodes as edges
        //         // Can do usual direction check to move along and find edge, 
        //         // but instead, when at end of a direction, check if at a corner and add the direction to that corner to a temp list
        //         // list prevents there being more than 4 sides in check



        //         return total;
        //     }
        // }


        private void FindRegionSize(Region region, Vector2Int pos, HashSet<Vector2Int> explored)
        {
            explored.Add(pos);
            foreach (Vector2Int direction in GridUtils.directions)
            {
                Vector2Int newPos = pos + direction;
                if (GridUtils.IsOutOfRange(newPos, xLen, yLen)) // If out of grid add permiter and ignore
                {
                    region.perimeter++;
                    continue;
                }
                if (explored.Contains(newPos)) continue; // If already explored ignore            

                // If going into a new region don't keep travelling that direction and increment permiter (as boundary found)
                if (grid[newPos.x, newPos.y] != region.identifier)
                {
                    region.perimeter++;
                }
                else // Else if in same region still then add to area and explore around that node
                {
                    region.area++;
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

                    char c = grid[x, y];
                    Region region = new(c);

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
            int totalPrice = 0;
            Region[] regions = GetRegions();

            foreach (Region region in regions)
            {
                totalPrice += region.Price;
            }

            return totalPrice.ToString();
        }

        public string? ExecPartB()
        {
            int totalPrice = 0;
            return totalPrice.ToString();
        }
        public void Setup(string[] input, string continuousInput)
        {
            grid = GridUtils.ConvertInputToGrid(input);
            xLen = grid.GetLength(0);
            yLen = grid.GetLength(1);
        }
    }
}