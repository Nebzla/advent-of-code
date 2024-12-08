using System.Text.RegularExpressions;
namespace _2024.src.Utils
{
    public static partial class ParsingUtils
    {
        private readonly static string inputDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "src", "inputs"));
        public static string[] GetInput(int day)
        {
            if (day < 1 || day > 25) throw new ArgumentException("Invalid day entered");
            return File.ReadAllLines(Path.Combine(inputDirectory, day + ".txt"));
        }

        public static string GetContinuousInput(int day)
        {
            if (day < 1 || day > 25) throw new ArgumentException("Invalid day entered");
            return File.ReadAllText(Path.Combine(inputDirectory, day + ".txt"));
        }


        public static int[][] ParseDigits(string[] rows)
        {
            Regex pattern = DigitsRegex();
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
            Regex pattern = DigitsRegex();
            List<List<int>> parsedRows = [];
            foreach(string row in rows)
            {
                List<int> matches = pattern.Matches(row).Select(d => int.Parse(d.Value)).ToList();
                parsedRows.Add(matches);
            }

            return [.. parsedRows];
        }

        public static IEnumerable<int> ParseDigits(string row)
        {
            Regex pattern = DigitsRegex();
            return pattern.Matches(row).Select(d => int.Parse(d.Value));
        }


        [GeneratedRegex(@"(\d)+")]
        private static partial Regex DigitsRegex();
    }
}



