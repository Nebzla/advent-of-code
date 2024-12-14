using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace _2024.src.Types
{
    public struct Vector2Int(int x, int y)
    {
        public int x = x;
        public int y = y;

        public static readonly Vector2Int Zero = new(0, 0);
        public static readonly Vector2Int Left = new(-1, 0);
        public static readonly Vector2Int TopLeft = new(-1, -1);
        public static readonly Vector2Int Up = new(0, -1);
        public static readonly Vector2Int TopRight = new(1, -1);
        public static readonly Vector2Int Right = new(1, 0);
        public static readonly Vector2Int BottomRight = new(1, 1);
        public static readonly Vector2Int Down = new(0, 1);
        public static readonly Vector2Int BottomLeft = new(-1, 1);

        public static readonly Vector2Int[] Directions = [Left, Up, Right, Down];
        public static readonly Vector2Int[] DiagonalDirections = [Left, TopLeft, Up, TopRight, Right, BottomRight, Down, BottomLeft];

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y - b.y);
        public static Vector2Int operator *(Vector2Int a, Vector2Int b) => new(a.x * b.x, a.y * b.y);
        public static Vector2Int operator /(Vector2Int a, Vector2Int b) => new(a.x / b.x, a.y / b.y);
        public static Vector2Int operator *(Vector2Int a, int b) => new(a.x * b, a.y * b);
        public static Vector2Int operator /(Vector2Int a, int b) => new(a.x / b, a.y / b);

        public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2Int a, Vector2Int b) => a.x != b.x || a.y != b.y;


        public override readonly bool Equals([NotNullWhen(true)] object? obj) => base.Equals(obj);
        public override readonly int GetHashCode() => HashCode.Combine(x, y);

        public readonly double Magnitude() => Math.Sqrt(x * x + y * y);

        public readonly Vector2 GetNormalizedVector()
        {
            double magnitude = Magnitude();
            return new Vector2((float)(x / magnitude), (float)(y / magnitude));
        }

        public readonly Vector2Int GetAbsoluteVector() => new(Math.Abs(x), Math.Abs(y));
        public readonly Vector2Int GetPerpendicularVector(bool clockwise = true) => new(clockwise ? y : -y, clockwise ? -x : x);
        public readonly bool IsDiagonal() => x != 0 && y != 0;

        public readonly double DistanceTo(Vector2Int other)
        {
            int dX = other.x - x;
            int dY = other.y - y;

            return Math.Sqrt(dX * dX + dY * dY);
        }

        public readonly Vector2Int DirectionTo(Vector2Int other) => other - this;
    }


    public struct Vector2Long(long x, long y)
    {
        public long x = x;
        public long y = y;

        public static Vector2Long operator +(Vector2Long a, Vector2Long b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Long operator -(Vector2Long a, Vector2Long b) => new(a.x - b.x, a.y - b.y);
        public static Vector2Long operator *(Vector2Long a, Vector2Long b) => new(a.x * b.x, a.y * b.y);
        public static Vector2Long operator /(Vector2Long a, Vector2Long b) => new(a.x / b.x, a.y / b.y);
        public static Vector2Long operator *(Vector2Long a, int b) => new(a.x * b, a.y * b);
        public static Vector2Long operator /(Vector2Long a, int b) => new(a.x / b, a.y / b);

        public static bool operator ==(Vector2Long a, Vector2Long b) => a.x == b.x && a.y == b.y;
        public static bool operator !=(Vector2Long a, Vector2Long b) => a.x != b.x || a.y != b.y;


        public override readonly bool Equals([NotNullWhen(true)] object? obj) => base.Equals(obj);
        public override readonly int GetHashCode() => HashCode.Combine(x, y);

        public readonly double Magnitude() => Math.Sqrt(x * x + y * y);

        public readonly Vector2 GetNormalizedVector()
        {
            double magnitude = Magnitude();
            return new Vector2((float)(x / magnitude), (float)(y / magnitude));
        }

        public readonly Vector2Long GetAbsoluteVector() => new(Math.Abs(x), Math.Abs(y));
        public readonly Vector2Long GetPerpendicularVector(bool clockwise = true) => new(clockwise ? y : -y, clockwise ? -x : x);
        public readonly bool IsDiagonal() => x != 0 && y != 0;

        public readonly double DistanceTo(Vector2Long other)
        {
            long dX = other.x - x;
            long dY = other.y - y;

            return Math.Sqrt(dX * dX + dY * dY);
        }

        public readonly Vector2Long DirectionTo(Vector2Long other) => other - this;
    }
}
