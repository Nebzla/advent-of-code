using _2024.src.Interfaces;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day11 : ISolution
    {
        public ushort DayNumber => 11;

        private List<long> initialStones = [];


        private static readonly Dictionary<long, long> cachedMultiplications = [];
        private static void GetNewStones(LinkedList<long> stones)
        {
            LinkedListNode<long>? stone = stones.First;

            while (stone != null)
            {
                if (stone.Value == 0) // If zero, flip to one
                {
                    stone.Value = 1;
                    stone = stone.Next;
                    continue;
                }

                int digitLength = (int)Math.Log10(stone.Value) + 1;
                if (digitLength % 2 == 0) // If even digits, split
                {
                    long divisor = (long)Math.Pow(10, digitLength / 2);
                    long leftStone = stone.Value / divisor;
                    long rightStone = stone.Value % divisor;

                    stone.Value = rightStone;
                    stones.AddBefore(stone, leftStone);
                    stone = stone.Next;
                    continue;
                }

                stone.Value *= 2024;// If no other conditions apply, multiply by 2024


                stone = stone.Next;
            }
        }


        public string? ExecPartA()
        {
            LinkedList<long> stones = new(initialStones);
            for (int i = 0; i < 25; ++i)
            {
                GetNewStones(stones);
            }

            return stones.Count.ToString();
        }
        public string? ExecPartB()
        {
            long total = 0;

            LinkedList<long> stones = new(initialStones);

            Console.WriteLine(stones.Count);

            foreach (long stone in stones) // Process one stone at a time so memory isnt full
            {
                LinkedList<long> stonesSubList = [];
                stonesSubList.AddFirst(stone);

                double setupTime = 0;
                for (int i = 0; i < 75; ++i)
                {
                    double currentTime = DiagnosticUtils.Benchmark(() => GetNewStones(stonesSubList), DiagnosticUtils.TimeUnit.Milliseconds);
                    Console.WriteLine($"Blink {i + 1} complete, took {currentTime}ms");
                    setupTime += currentTime;
                }

                Console.WriteLine($"\nNumber {stone} complete, took {setupTime}ms\n");
                total += stonesSubList.Count;
            }

            return total.ToString();
        }
        public void Setup(string[] input, string continuousInput)
        {
            initialStones = [.. ParsingUtils.ParseDigits(continuousInput)];
        }
    }
}