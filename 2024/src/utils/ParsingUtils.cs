namespace _2024.src.utils
{
    public static class ParsingUtils
    {
        public static string[] GetInput(int day)
        {
            if (day < 1 || day > 25) throw new ArgumentException("Invalid day entered");
            return File.ReadAllLines($"../2024/src/inputs/{day}.txt");
        }
    }
}



