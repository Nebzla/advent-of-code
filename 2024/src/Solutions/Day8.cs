using _2024.src.Interfaces;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day8 : ISolution
    {
        public ushort DayNumber => 8;

        private char[,] grid = new char[0, 0];
        private int xLen;
        private int yLen;

        private readonly HashSet<(int x, int y)> antennaLocations = [];

        private HashSet<(int x, int y)> GetAntiNodes()
        {
            HashSet<(int x, int y)> antiNodes = [];
            
            // Get each antenna location, and compare it to every other antenna to see if it is possible to insert an antinode
            foreach((int x, int y) currentAntenna in antennaLocations) 
            {
                foreach((int x, int y) comparisonAntenna in antennaLocations)
                {
                    // Get new anti node position by get direction from current to all other vectors and adding that to other, and if in bounds, is valid
                    if(comparisonAntenna == currentAntenna) continue;
                    if(grid[currentAntenna.x, currentAntenna.y] != grid[comparisonAntenna.x, comparisonAntenna.y]) continue;

                    (int x, int y) antiNode = GridUtils.AddVectors(comparisonAntenna, GridUtils.GetDirectionVector(currentAntenna, comparisonAntenna));
                    if(antiNode.x < 0 || antiNode.x >= xLen || antiNode.y < 0 || antiNode.y >= yLen) continue;

                    antiNodes.Add(antiNode);
                }
            }

            return antiNodes;
        }

        private HashSet<(int x, int y)> GetResonantAntiNodes()
        {
            HashSet<(int x, int y)> antiNodes = [];
            
            // Get each antenna location, and compare it to every other antenna to see if it is possible to insert an antinode
            foreach((int x, int y) currentAntenna in antennaLocations) 
            {
                foreach((int x, int y) comparisonAntenna in antennaLocations)
                {
                    // Get new anti node position by get direction from current to all other vectors and adding that to other, and if in bounds, is valid
                    if(comparisonAntenna == currentAntenna) continue;
                    if(grid[currentAntenna.x, currentAntenna.y] != grid[comparisonAntenna.x, comparisonAntenna.y]) continue;

                    antiNodes.Add(currentAntenna);
                    antiNodes.Add(comparisonAntenna);

                    (int x, int y) direction = GridUtils.GetDirectionVector(currentAntenna, comparisonAntenna);

                    (int anX, int anY) = GridUtils.AddVectors(comparisonAntenna, direction);
                    while(anX >= 0 && anX < xLen && anY >= 0 && anY < yLen)
                    {
                        antiNodes.Add((anX, anY));
                        (anX, anY) = GridUtils.AddVectors((anX, anY), direction);
                    }
                }
            }

            return antiNodes;
        }

        public string? ExecPartA()
        {
            return GetAntiNodes().Count.ToString();
        }

        public string? ExecPartB()
        {
            return GetResonantAntiNodes().Count.ToString();
        }

        public void Setup(string[] input, string continuousInput)
        {
            grid = GridUtils.ConvertInputToGrid(input);
            xLen = grid.GetLength(0);
            yLen = grid.GetLength(1);

            for (int x = 0; x < xLen; ++x)
            {
                for (int y = 0; y < yLen; ++y)
                {
                    if (grid[x, y] != '.') antennaLocations.Add((x, y));
                }
            }
        }
    }
}