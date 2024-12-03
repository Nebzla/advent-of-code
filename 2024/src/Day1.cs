using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024.src
{
    public class Day1 : ISolution
    {
        public ushort DayNumber => 1;


        string[] input = ParsingUtils.GetInput(1);
        readonly List<int> leftList = [];
        readonly List<int> rightList = [];

        private void PopulateLists()
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

        private long SumLists()
        {
            long total = 0;
            for(int i = 0; i < leftList.Count; ++i)
            {
                total += Math.Abs(leftList[i] - rightList[i]);
            }

            return total;
        }

        private long GetSimilarityScore()
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

        public void Setup(string[] input, string continuousInput) 
        {
            this.input = input;
            PopulateLists();
        }

        public string ExecPartA()
        {
            return SumLists().ToString();
        }

        public string ExecPartB()
        {
            return GetSimilarityScore().ToString();
        }
    }
}