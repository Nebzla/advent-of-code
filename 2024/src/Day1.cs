using _2024.src.utils;

namespace _2024.src
{
    public static class Day1
    {
        static readonly string[] input = ParsingUtils.GetInput(1);
        static readonly List<int> leftList = [];
        static readonly List<int> rightList = [];

        private static void PopulateLists()
        {
            foreach(string line in input)
            {
                string[] split = line.Split("   ");
                leftList.Add(int.Parse(split[0]));
                rightList.Add(int.Parse(split[1]));
            }

            leftList.Sort();
            rightList.Sort();
        }

        private static long SumLists()
        {
            long total = 0;
            for(int i = 0; i < leftList.Count; ++i)
            {
                total += Math.Abs(leftList[i] - rightList[i]);
            }

            return total;
        }

        private static long GetSimilarityScore()
        {
            long score = 0;
            for(int l = 0; l < leftList.Count; ++l)
            {
                int occurences = 0;
                for(int r = 0; r < rightList.Count; ++r)
                {
                    if(rightList[r] == leftList[l]) ++occurences;
                }

                score += leftList[l] * occurences;
            }

            return score;
        }

        public static void Execute()
        {
            PopulateLists();
            Console.WriteLine("Part 1: " + SumLists());
            Console.WriteLine("Part 2: " + GetSimilarityScore());
        }
    }
}