namespace _2024.src.Types
{
    public readonly struct Matrix2x2
    {
        public readonly int[,] matrix = new int[2, 2];


        private static bool IsValidMatrix(int[,] matrix)
        {
            return matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2;
        }

        public Matrix2x2(int[,] matrix)
        {
            if(!IsValidMatrix(matrix)) throw new ArgumentException("Invalid matrix dimensions");
            this.matrix = matrix;
        }

        public int Determinant => (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);

        public static int GetDeterminant(int[,] matrix)
        {
            if(!IsValidMatrix(matrix)) throw new ArgumentException("Invalid matrix dimensions");
            return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
        }
    }
}