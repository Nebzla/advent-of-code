using _2024.src.Interfaces;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day11 : ISolution
    {
        public ushort DayNumber => 11;

        private HashSet<long> initialStones = [];

        // Causes memory to get full very quickly, inneficient solution left for completeness
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


        private static void AddOrIncrementDict(Dictionary<long, long> dict, long key, long count = 1)
        {
            if (dict.TryAdd(key, count)) return;
            dict[key] += count;
        }

        // left will always be defined, right will be defined if it has split
        private static readonly Dictionary<long, (long left, long? right)> calculationCache = [];

        private static long GetTotalStones(HashSet<long> stones, ushort blinks)
        {
            Dictionary<long, long> stoneDict = stones.ToDictionary(stone => stone, c => 1L);

            for (ushort i = 0; i < blinks; ++i)
            {
                Dictionary<long, long> tempDict = [];
                foreach (KeyValuePair<long, long> stone in stoneDict)
                {
                    long initialValue = stone.Key;
                    long initialCount = stone.Value;

                    // If calculation has already been done, get its result instead of performing it again
                    if (calculationCache.TryGetValue(initialValue, out (long left, long? right) cachedResult))
                    {
                        // Add results to dictionary, or increment count if results already exist
                        AddOrIncrementDict(tempDict, cachedResult.left, initialCount);
                        if (cachedResult.right != null) AddOrIncrementDict(tempDict, cachedResult.right ?? throw new NullReferenceException(), initialCount);
                    }
                    else
                    {
                        if (initialValue == 0) // If zero flip to 1
                        {
                            AddOrIncrementDict(tempDict, 1, initialCount);
                            calculationCache.Add(initialValue, (1, null));
                            continue;
                        }

                        int digitLength = (int)Math.Log10(initialValue) + 1;
                        if (digitLength % 2 == 0) // If even digits split into 2
                        {
                            long divisor = (long)Math.Pow(10, digitLength / 2);
                            long leftStone = initialValue / divisor;
                            long rightStone = initialValue % divisor;

                            AddOrIncrementDict(tempDict, leftStone, initialCount);
                            AddOrIncrementDict(tempDict, rightStone, initialCount);
                            calculationCache.Add(initialValue, (leftStone, rightStone));
                            continue;
                        }

                        long multipliedVal = initialValue * 2024;
                        AddOrIncrementDict(tempDict, multipliedVal, initialCount); // Otherwise multiply by 2024
                        calculationCache.Add(initialValue, (multipliedVal, null));
                    }
                }

                stoneDict = tempDict;
            }

            long total = 0;
            foreach(KeyValuePair<long, long> stone in stoneDict) total += stone.Value;
            return total;
        }


        

        public string? ExecPartA() => GetTotalStones(new(initialStones), 25).ToString();
        public string? ExecPartB() => GetTotalStones(new(initialStones), 75).ToString();

        public void Setup(string[] input, string continuousInput)
        {
            initialStones = [.. ParsingUtils.ParseDigits(continuousInput)];
        }
    }
}