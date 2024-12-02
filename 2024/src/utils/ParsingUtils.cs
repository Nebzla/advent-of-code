using System.Text.RegularExpressions;
namespace _2024.src.utils
{
    public static partial class ParsingUtils
    {
        public static string[] GetInput(int day)
        {
            if (day < 1 || day > 25) throw new ArgumentException("Invalid day entered");
            return File.ReadAllLines($"D:/Programming/advent-of-code/2024/src/inputs/{day}.txt");
        }

        public static int[][] ParseDigits(string[] rows)
        {
            Regex pattern = MyRegex();
            List<int[]> parsedRows = [];
            foreach(string row in rows)
            {
                int[] matches = pattern.Matches(row).Select(d => int.Parse(d.Value)).ToArray();
                parsedRows.Add(matches);
            }

            return [.. parsedRows];
        }

        public static List<int>[] ParseDigitsList(string[] rows)
        {
            Regex pattern = MyRegex();
            List<List<int>> parsedRows = [];
            foreach(string row in rows)
            {
                List<int> matches = pattern.Matches(row).Select(d => int.Parse(d.Value)).ToList();
                parsedRows.Add(matches);
            }

            return [.. parsedRows];
        }

        [GeneratedRegex(@"(\d)+")]
        private static partial Regex MyRegex();
    }
}



