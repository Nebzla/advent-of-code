using _2024.src.Interfaces;

namespace _2024.src.Utils
{
    public static class ArrayUtils
    {
        public static T[] DeepCopyArray<T>(T[] arr) where T : IDeepCopyable<T>
        {
            T[] copy = new T[arr.Length];

            for(int i = 0; i < arr.Length; ++i)
            {
                copy[i] = arr[i].DeepCopy();
            }

            return copy;
        }
        
        public static T[,] DeepCopyMultiDimensionalArray<T>(T[,] arr)
        {
            int xLen = arr.GetLength(0);
            int yLen = arr.GetLength(1);
            
            T[,] copy = new T[xLen, yLen];

            for (int x = 0; x < xLen; ++x)
            {
                for(int y = 0; y < yLen; ++y)
                {
                    copy[x, y] = arr[x, y];
                }
            }

            return copy;
        }

        public static T[][] DeepCopy2DArray<T>(T[][] arr)
        {
            T[][] copy = new T[arr.Length][];
            for (int i = 0; i < arr.Length; ++i)
            {
                copy[i] = new T[arr[i].Length];
                for (int j = 0; j < arr[i].Length; ++j)
                {
                    copy[i][j] = arr[i][j];
                }
            }

            return copy;
        }
    }
}