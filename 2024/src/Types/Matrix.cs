namespace _2024.src.Types
{
    public readonly struct SquareMatrix
    {
        private readonly long [,] _matrix;
        public readonly long Size { get; }
        public readonly double Determinant => GetDeterminant();

        private void ValidateIndicies(long r, long c)
        {
            if(r < 0 || r >= Size || c < 0 || c >= Size) throw new IndexOutOfRangeException("Matrix index is out of range");
        }

        private double GetDeterminant()
        {
            if(Size == 1) return _matrix[0, 0];
            if(Size == 2) return (_matrix[0,0] * _matrix[1, 1]) - (_matrix[0, 1] * _matrix[1, 0]);

            throw new NotImplementedException(); // Can be implemented recursively for bigger matrices but I cba because my brain can't cope
        }


        public SquareMatrix GetMinorMatrix(long removalRow, long removalColumn)
        {
            ValidateIndicies(removalRow, removalColumn);
            SquareMatrix minorMatrix = new(Size - 1);
            long minorRow = 0; // Track of index in minor matrix, so it is known where to copy elements to
            
            for(long r = 0; r < Size; ++r)
            {
                if(r == removalRow) continue; // Skip if row needs removing

                long minorColumn = 0;
                for(long c = 0; c < Size; ++c)
                {
                    if(c == removalColumn) continue; // Skip if column needs removing

                    minorMatrix[minorRow, minorColumn] = _matrix[r, c];
                    minorColumn ++;
                }

                minorRow ++;
            }

            return minorMatrix;
        }


        public SquareMatrix(long size, long[]? values = null) // Values entered as read
        {
            if(size <= 0) throw new ArgumentException("Invalid matrix size entered");
            Size = size;
            _matrix = new long[size, size];

            if(values != null)
            {
                if(values.Length != size * size) throw new ArgumentException("Initial SquareMatrix values don't match size");

                long valueIndex = 0;

                for(long r = 0; r < size; ++r)
                {
                    for(long c = 0; c < size; ++c)
                    {
                        _matrix[r, c] = values[valueIndex];
                        ++valueIndex;
                    }
                }
            }
        }

        public long this[long r, long c]
        {
            get
            {
                ValidateIndicies(r, c);
                return _matrix[r, c];
            }
            set
            {
                ValidateIndicies(r, c);
                _matrix[r, c] = value;
            }
        }
    }
}