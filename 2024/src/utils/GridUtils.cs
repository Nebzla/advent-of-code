namespace _2024.src.Utils
{
    public static class GridUtils
    {
        public static char[,] ConvertInputToGrid(string[] input)
        {
            if (input.Length == 0 || input[0].Length == 0) return new char[0, 0];
            char[,] grid = new char[input[0].Length, input.Length];

            for (int x = 0; x < input.Length; ++x)
            {
                for (int y = 0; y < input[0].Length; ++y)
                {
                    grid[x, y] = input[y][x];
                }
            }

            return grid;
        }

        public static int[,] ConvertIntInputToGrid(string[] input)
        {
            if (input.Length == 0 || input[0].Length == 0) return new int[0, 0];
            int[,] grid = new int[input[0].Length, input.Length];

            for (int x = 0; x < input.Length; ++x)
            {
                for (int y = 0; y < input[0].Length; ++y)
                {
                    char c = input[y][x];
                    if (!char.IsDigit(c)) grid[x, y] = -1;
                    else grid[x, y] = c - '0';
                }
            }

            return grid;
        }


        // Legacy Utilities, Should use Vector2Int struct from now on for all vector calculations
        public static (int x, int y) GetDirectionVector((int x, int y) pos1, (int x, int y) pos2, int multiplier = 1)
        {
            return ((pos2.x - pos1.x) * multiplier, (pos2.y - pos1.y) * multiplier);
        }

        public static (int x, int y) AddVectors((int x, int y) vect1, (int x, int y) vect2)
        {
            return (vect1.x + vect2.x, vect1.y + vect2.y);
        }

        public static (int x, int y) MultiplyDirectionVectors((int x, int y) vect1, (int x, int y) vect2)
        {
            return (vect1.x * vect2.x, vect1.y * vect2.y);
        }

        public static (int x, int y) MultiplyDirectionVector((int x, int y) vect, int multiplier)
        {
            return (vect.x * multiplier, vect.y * multiplier);
        }
    }
}