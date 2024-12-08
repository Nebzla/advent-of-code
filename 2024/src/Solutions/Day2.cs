using _2024.src.Interfaces;
using _2024.src.Utils;

namespace _2024.src.Solutions
{
    public class Day2 : ISolution
    {
        public ushort DayNumber => 2;
        
        private List<int>[] nums = [];

        private static bool CheckValid(List<int> row)
        {
            if(row.Count == 0) return false;
            if(row.Count < 2) return true; // If row only has two elements it can always be fixed by removing one

            int? previousDifference = null;

            for(int i = 0; i < row.Count - 1; ++i)
            {
                int difference = row[i + 1] - row[i];
                if(difference == 0) return false; // If no change then invalid

                if(!GeneralUtils.HasSameSign(difference, previousDifference ?? 0)) return false; // If difference has changed from +ve to -ve then invalid
                if(Math.Abs(difference) > 3) return false; // If |difference| is more than 3 then invalid

                previousDifference = difference;
            }

            return true;
        }

        private static bool BruteForceValidate(List<int> row)
        {
            for(int i = 0; i < row.Count; ++i)
            {
                List<int> rowCopy = [.. row];
                rowCopy.RemoveAt(i);
                if(CheckValid(rowCopy)) return true;
            }

            return false;
        }




        public string? ExecPartA()
        {
            int total = 0;
            foreach(List<int> row in nums)
            {
                if(CheckValid(row)) ++total;
            }

            return total.ToString();
        }

        public string? ExecPartB()
        {
            int total = 0;

            foreach(List<int> row in nums)
            {
                if(BruteForceValidate(row)) ++total;
            }

            return total.ToString();
        }

        public void Setup(string[] input, string continuousInput) 
        {   
            nums = ParsingUtils.ParseDigitsList(input);
        }



    }
}

