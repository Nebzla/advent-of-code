using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace _2024.src.Types
{
    public struct Vector2Int(int x, int y)
    {
        public int x = x;
        public int y = y;

        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y + b.y);
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

        public readonly double DistanceTo(Vector2Int other)
        {
            int dX = other.x - x;
            int dY = other.y - y;

            return Math.Sqrt(dX * dX + dY * dY);
        }

        public readonly Vector2Int DirectionTo(Vector2Int other) => other - this;
    }
}
