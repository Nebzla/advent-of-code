using _2024.src.Interfaces;

namespace _2024.src
{
    public partial class Day4 : ISolution
    {
        public ushort DayNumber => 4;

        string[] input = [];
        int xLen; //Horizontal
        int yLen; //Vertical (Downwards)

        private static readonly char[] xmasString = ['X', 'M', 'A', 'S'];
        private int GetStringsAtPosition(int xO, int yO)
        {
            int total = 0;
            for(int y = -1; y <= 1; ++y)
            {
                for(int x = -1; x <= 1; ++x)
                {
                    if(x == 0 && y == 0) continue;
                    // (x, y) = direction to search relative to current position (xO,yO)

                    // If will end up splilling outside bounds of array, continue to next iteration
                    int xEnd = xO + (x * (xmasString.Length - 1));
                    int yEnd = yO + (y * (xmasString.Length - 1));

                    if(xEnd >= xLen || xEnd < 0 || yEnd >= yLen || yEnd < 0) continue;

                    for(int i = 1; i < xmasString.Length; ++i) // i = multiplier for offset of (x, y)
                    {           
                        int xNew = xO + (x * i);
                        int yNew = yO + (y * i);
                        char character = input[yNew][xNew];

                        if(character != xmasString[i]) break; // If doesnt follow pattern stop iterating down path
                        if(i == xmasString.Length - 1) ++total; // If made to last index and is valid, add to total as full string located
                    }
                }
            }
            return total;
        }

        private bool HasCross(int x0, int y0)
        {
            if(x0 - 1 < 0 || x0 + 1 >= xLen || y0 - 1 < 0 || y0 + 1 >= yLen) return false;
            char TL = input[y0 - 1][x0 + 1];
            char TR = input[y0 + 1][x0 + 1];
            char BL = input[y0 - 1][x0 - 1];
            char BR = input[y0 + 1][x0 - 1];

            return ((TL == 'M' && BR == 'S') || (TL == 'S' && BR == 'M')) && ((TR == 'M' && BL == 'S') || (TR == 'S' && BL == 'M'));
        }

        private int GetTotalXmasStrings()
        {
            int total = 0;
            char query = xmasString[0];
            for(int r = 0; r < yLen; ++r)
            {
                for(int c = 0; c < xLen; ++c)
                {
                    if(input[r][c] != query) continue;
                    total += GetStringsAtPosition(c, r);
                }
            }
            return total;
        }


        private int GetTotalCrosses()
        {
            int total = 0;
            for(int r = 0; r < yLen; ++r)
            {
                for(int c = 0; c < xLen; ++c)
                {
                    if(input[r][c] != 'A') continue;
                    if(HasCross(c, r)) ++total;
                }
            }
            return total;
        }


        public string? ExecPartA()
        {
            return GetTotalXmasStrings().ToString();
        }

        // I hate these fucking elves
        public string? ExecPartB()
        {
            return GetTotalCrosses().ToString();
        }

        public void Setup(string[] input, string continuousInput)
        {
            if(input.Length == 0) return;
            this.input = input;
            xLen = input[0].Length;
            yLen = input.Length;
        }
    }
}