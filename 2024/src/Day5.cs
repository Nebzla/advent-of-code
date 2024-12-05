using _2024.src.Interfaces;
using _2024.src.utils;

namespace _2024.src
{
    public class Day5 : ISolution
    {
        public ushort DayNumber => 5;

        readonly Dictionary<int, HashSet<int>> forwardRules = []; // A dictionary of all numbers containing rules, and all numbers that must come after them
        readonly Dictionary<int, HashSet<int>> backwardRules = []; // A dictionary of all numbers containing rules, and all numbers that must come after them

        List<int>[] values = [];

        private bool IsValidLine(List<int> line)
        {
            for(int i = 0; i < line.Count; ++i)
            {
                int value = line[i];
                if(forwardRules.TryGetValue(value, out HashSet<int>? forwardRule)) 
                {
                    foreach(int num in forwardRule)
                    {
                        int numIndex = line.IndexOf(num);
                        if(numIndex == -1) continue;
                        if(numIndex < i) return false; // If num exists in values, and is mistakenly before query, is invalid
                    }
                }

                if(backwardRules.TryGetValue(value, out HashSet<int>? backwardRule))
                {
                    foreach(int num in backwardRule)
                    {
                        int numIndex = line.IndexOf(num);
                        if(numIndex == -1) continue;
                        if(numIndex > i) return false; // If num exists in values, and is mistakenly before query, is invalid
                    }
                }

            }

            return true;
        }

        private int GetLineTotals()
        {
            int total = 0;
            foreach(List<int> value in values)
            {
                if(value.Count == 0) continue;
                if(IsValidLine(value)) total += value[value.Count / 2];
            }
            return total;
        }

        private List<List<int>> GetInvalidLines()
        {
            List<List<int>> invalidLines = [];
            foreach(List<int> value in values)
            {
                if(value.Count == 0) continue;
                if(!IsValidLine(value)) invalidLines.Add(value);
            }

            return invalidLines;
        }
        
        private void FixInvalidLine(List<int> line)
        {
            for(int p = 0; p < line.Count; ++p)
            {
                int pivot = line[p];
                HashSet<int> forwardRule = forwardRules[pivot];
                HashSet<int> backwardRule = backwardRules[pivot];

                if(forwardRule.Count == 0 && backwardRule.Count == 0) continue;

                //TODO Sort potential bounds errors
                for(int i = 0; i < line.Count; ++i)
                {
                    int num = line[i];
                    if(i == p) continue;
                    // If the num needs to be moved after or before the current pivot, delete it from its old pos and insert it into its new position
                    if(p != line.Count - 1 && i < p && forwardRule.Contains(num)) 
                    {
                        line.Insert(p + 1, num);
                        line.RemoveAt(i);
                    }
                    if(p != 0 && i > p && backwardRule.Contains(num))
                    {
                        line.Insert(p - 1, num);
                        line.RemoveAt(i + 1);
                    }
                }
            }
        }


        public string? ExecPartA()
        {
            return GetLineTotals().ToString();
        }

        public string? ExecPartB()
        {
            return null;
        }

        public void Setup(string[] input, string continuousInput)
        {
            string[] parts = continuousInput.Split("\n\n");
            if(parts.Length != 2) return;

            string[] ruleStrings = parts[0].Split('\n');
            string[] valueStrings = parts[1].Split('\n');

            foreach(string rule in ruleStrings)
            {
                string[] split = rule.Split('|');
                if(split.Length != 2) return;
                
                int first = int.Parse(split[0]);
                int last = int.Parse(split[1]);

                if(forwardRules.TryGetValue(first, out HashSet<int>? forwardKeys)) forwardKeys.Add(last);
                else forwardRules.Add(first, new(last));

                if(backwardRules.TryGetValue(last, out HashSet<int>? backwardKeys)) backwardKeys.Add(first);
                else backwardRules.Add(last, new(first));
            }

            values = ParsingUtils.ParseDigitsList(valueStrings);
        }
    }
}