using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024.src.Solutions
{
    public class Day7 : ISolution
    {
        public ushort DayNumber => 7;

        private Line[] input = [];

        private readonly struct Line(long t, int[] n)
        {
            public readonly long target = t;
            public readonly int[] nums = n;
        }


        private static bool CheckLine(int[] nums, long target, char[] operators)
        {
            // Get all combinations of operators between numbers
            List<string> permutations = GeneralUtils.GetPermutations(operators, nums.Length - 1);
            foreach(string permutation in permutations)
            {
                long total = nums[0];

                for(int i = 1; i < nums.Length; ++i)
                {
                    char op = permutation[i - 1];

                    if(op == '|') total = long.Parse(total.ToString() + nums[i].ToString()); // Concact as string and then convert back
                    else if(op == '+') total += nums[i];
                    else total *= nums[i];
                }

                if(total == target) return true;
            }

            return false;
        }





        public string? ExecPartA()
        {
            long total = 0;
            char[] operators = ['+', '*'];
            foreach(Line line in input)
            {
                if(CheckLine(line.nums, line.target, operators)) total += line.target;
            }

            return total.ToString();
        }

        public string? ExecPartB()
        {
            long total = 0;
            char[] operators = ['+', '*', '|'];
            foreach(Line line in input)
            {
                if(CheckLine(line.nums, line.target, operators)) total += line.target;
            }

            return total.ToString();
        }

        public void Setup(string[] input, string continuousInput)
        {
            List<Line> inputList = [];
            foreach(string line in input)
            {
                string[] parts = line.Split(":");
                if(parts.Length != 2) continue;

                long target = long.Parse(parts[0]);
                int[] nums = ParsingUtils.ParseDigits(parts[1]).ToArray();
                inputList.Add(new(target, nums));
            }

            this.input = [.. inputList];
        }

    }
}