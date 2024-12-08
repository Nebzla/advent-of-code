using System.Text.RegularExpressions;
using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024.src.Solutions
{
    public partial class Day3 : ISolution
    {
        public ushort DayNumber => 3;
        
        private List<string> multiplications = [];
        private List<string> operations = [];

        private static int Multiply(string operation)
        {
            int[] nums = ParsingUtils.ParseDigits(operation).ToArray();
            if (nums.Length != 2) throw new InvalidOperationException("Only 2 numbers should exist in each multiplication");
            return nums[0] * nums[1];
        }

        private long SumMultipliedInput()
        {
            long total = 0;
            foreach (string multiplication in multiplications)
            {
                total += Multiply(multiplication);
            }

            return total;
        }

        private long SumToggledInput()
        {
            long total = 0;
            bool canMultiply = true;

            foreach(string operation in operations)
            {
                if(operation == "do()") canMultiply = true;
                else if(operation == "dont()" || operation == "don't()") canMultiply = false;
                else if(canMultiply) total += Multiply(operation);
            }
            
            return total;
        }
        


        public string? ExecPartA()
        {
            return SumMultipliedInput().ToString();
        }

        public string? ExecPartB()
        {
            return SumToggledInput().ToString();
        }

        public void Setup(string[] input, string continuousInput)
        {
            multiplications = GetMultiplyFunctions().Matches(continuousInput).Select(v => v.Value).ToList();
            operations = GetMultiplyToggleFunctions().Matches(continuousInput).Select(v => v.Value).ToList();
        }

        [GeneratedRegex(@"(mul\(\d+,\d+\))")]
        private static partial Regex GetMultiplyFunctions();

        [GeneratedRegex(@"(mul\(\d+,\d+\))|(do\(\))|(don[']?t\(\))")]
        private static partial Regex GetMultiplyToggleFunctions();
    }
}