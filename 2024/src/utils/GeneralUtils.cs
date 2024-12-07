namespace _2024.src.utils
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

        public static T[][] DeepCopy2DArray<T>(T[][] arr)
        {
            T[][] copy = new T[arr.Length][];
            for(int i = 0; i < arr.Length; ++i)
            {
                copy[i] = new T[arr[i].Length];
                for(int j = 0; j < arr[i].Length; ++j)
                {
                    copy[i][j] = arr[i][j];
                }
            }
            
            return copy;
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