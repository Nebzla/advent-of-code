namespace _2024.src.Interfaces
{
    public interface ISolution
    {
        public static ushort DayNumber { get; }

        public string ExecPartA();
        public string ExecPartB();
        public void Setup(string[] input);
    }
}