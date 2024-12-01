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
    }
}