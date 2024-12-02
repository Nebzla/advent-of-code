using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024.src
{
    public class Day2 : ISolution
    {
        public ushort DayNumber => 2;
        List<int>[] nums = [];

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

        //!Non functional non-brute force approach, test data gives an answer that is 2 off from correct
        private static bool CheckFullValidity(List<int> initialRow)
        {
            if(initialRow.Count == 0) return false;
            if(initialRow.Count < 3) return true; // If row only has two elements it can always be fixed by removing one

            int? previousDifference = null;
            bool hasFixed = false; // If requires fixing twice, then still invalid

            List<int> row = [.. initialRow];

            for(int i = 0; i < row.Count - 1; ++i)
            {
                int difference = row[i + 1] - row[i];

                if(difference == 0) // If difference is zero, can remove first one then check same index again due to shortened list
                {
                    if(hasFixed) return false; // If problem already occured, cannot again
                    if(i == row.Count - 2) return true; // If now at end, there can be no more mistakes

                    row.RemoveAt(i + 1);
                    
                    --i; // Decrement to check same spot again
                    hasFixed = true;
                    continue;
                }

                if(!GeneralUtils.HasSameSign(difference, previousDifference ?? 0)) // Direction of sequence cannot change
                {
                    if(hasFixed) return false; // If problem already occured, cannot again
                    if(i == row.Count - 2) return true; // If now at end, there can be no more mistakes

                    // If later value is the same must remove this  one instead
                    if(i + 1 < row.Count && row[i + 2] == row[i]) row.RemoveAt(i);
                    else if(i == 1) // In this case it is possible to fix by removing the first element, rather than the usual i + 1
                    {
                        // If the order continuing past problem is the same as original, then the usual culprit can be removed, otherwise can remove zero
                        if(!GeneralUtils.HasSameSign(row[i + 2] - row[i + 1], previousDifference ?? throw new NullReferenceException()))
                        {
                            
                            row.RemoveAt(0);
                            previousDifference = -previousDifference;
                        }

                        row.RemoveAt(i + 1);
                    } else row.RemoveAt(i + 1);
                    
                    --i; // Decrement to check same spot again
                    hasFixed = true;
                    continue;
                }

                if(Math.Abs(difference) > 3) // Difference cannot be greater than 3
                {
                    if(hasFixed) return false; // If problem already occured, cannot again
                    if(i == row.Count - 2) return true; // If now at end, there can be no more mistakes


                    if(i == 0 && row.Count > 3)
                    {
                        int finalDirection = row[3] - row[2];
                        if(finalDirection == 0) return false;
                        
                        if(finalDirection > 0) // Ascending
                        {
                            if(row[2] > row[1]) row.RemoveAt(0);
                            else row.RemoveAt(1);
                        } else
                        {
                            if(row[2] > row[1]) row.RemoveAt(1);
                            else row.RemoveAt(0); 
                        }
                    } else row.RemoveAt(i + 1);
                    

                    --i; // Decrement to check same spot again
                    hasFixed = true;
                    continue;
                }
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


        public string ExecPartA()
        {
            int total = 0;
            foreach(List<int> row in nums)
            {
                if(CheckValid(row)) ++total;
            }

            return total.ToString();
        }

        public string ExecPartB()
        {
            HashSet<List<int>> hash = [];

            int total = 0;

            foreach(List<int> row in nums)
            {
                if(BruteForceValidate(row))
                {
                    hash.Add(row);
                } 
            }

            foreach(List<int> row in nums)
            {
                if(CheckFullValidity(row))
                {
                    ++total;
                    if(!hash.Contains(row)) GeneralUtils.PrintCSV(row);
                } 
            }

            return total.ToString();
        }



        public void Setup(string[] input) 
        {   
            nums = ParsingUtils.ParseDigitsList(input);
        }



    }
}

