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
    }
}