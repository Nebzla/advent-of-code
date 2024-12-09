namespace _2024.src.Utils
{
    public static class GeneralUtils
    {
        public static void Print<T>(IEnumerable<T> list)
        {
            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }
        public static void PrintCSV<T>(IEnumerable<T> list)
        {
            Console.WriteLine(string.Join(", ", list));
        }

        public static bool HasSameSign(int x, int y)
        {
            return (x >= 0 && y >= 0) || (x <= 0 && y <= 0);
        }

        private static void Permute(char[] symbols, int length, List<string> permutations, string current = "")
        {
            if(current.Length == length) // If full permutation is built, add it as a permutation
            {
                permutations.Add(current);
                return;
            }

            foreach(char symbol in symbols)
            {
                Permute(symbols, length, permutations, current + symbol); // Build next char recursively with all possibilities of symbols
            }
        }

        private static readonly Dictionary<(char[] symbols, int length), List<string>> cachedPermutations = [];
        public static List<string> GetPermutations(char[] symbols, int length)
        {
            // if has been called previously with same paramaters, no need to recalculate permutations
            if(cachedPermutations.TryGetValue((symbols, length), out List<string>? existingPermutation)) return existingPermutation;
            if(length < 1) return [];

            List<string> permutations = [];
            Permute(symbols, length, permutations);
            cachedPermutations.Add((symbols, length), permutations);
            return permutations;
        }
    }
}