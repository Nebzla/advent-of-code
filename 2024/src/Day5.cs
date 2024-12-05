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
            for (int i = 0; i < line.Count; ++i)
            {
                int value = line[i];
                if (forwardRules.TryGetValue(value, out HashSet<int>? forwardRule))
                {
                    foreach (int num in forwardRule)
                    {
                        int numIndex = line.IndexOf(num);
                        if (numIndex == -1) continue;
                        if (numIndex < i) return false; // If num exists in values, and is mistakenly before query, is invalid
                    }
                }

                if (backwardRules.TryGetValue(value, out HashSet<int>? backwardRule))
                {
                    foreach (int num in backwardRule)
                    {
                        int numIndex = line.IndexOf(num);
                        if (numIndex == -1) continue;
                        if (numIndex > i) return false; // If num exists in values, and is mistakenly before query, is invalid
                    }
                }

            }

            return true;
        }

        private int GetLineTotals()
        {
            int total = 0;
            foreach (List<int> value in values)
            {
                if (value.Count == 0) continue;
                if (IsValidLine(value)) total += value[value.Count / 2];
            }
            return total;
        }

        private List<List<int>> GetInvalidLines()
        {
            List<List<int>> invalidLines = [];
            foreach (List<int> value in values)
            {
                if (value.Count == 0) continue;
                if (!IsValidLine(value)) invalidLines.Add(value);
            }

            return invalidLines;
        }

        private void FixInvalidLine(List<int> line)
        {
            int len = line.Count;
            while (!IsValidLine(line))
            {
                for (int p = 0; p < len; ++p)
                {
                    int pivot = line[p];

                    bool forwardRulesExist = forwardRules.TryGetValue(pivot, out HashSet<int>? forwardRule);
                    bool backwardRulesExist = backwardRules.TryGetValue(pivot, out HashSet<int>? backwardRule);

                    // If pivot doesn't have any rules associated with it and doesn't care where it is, can continue
                    if (!forwardRulesExist && !backwardRulesExist) continue;


                    // For all numbers that are before the pivot that shouldn't be, insert them after
                    if (forwardRulesExist)
                    {
                        for (int i = 0; i < p; ++i)
                        {
                            int num = line[i];
                            if (!forwardRule!.Contains(num)) continue;

                            if (p != len - 1) line.Insert(p + 1, num); // Cant insert at end so need to add
                            else line.Add(num);

                            line.RemoveAt(i);
                        }
                    }

                    // For all numbers that are after the pivot that shouldn't be, insert them before
                    if (backwardRulesExist)
                    {
                        for (int i = p + 1; i < len; ++i)
                        {
                            int num = line[i];
                            if (!backwardRule!.Contains(num)) continue;
                            line.Insert(p, num);
                            line.RemoveAt(i + 1);
                        }
                    }
                }
            }
        }


        public string? ExecPartA()
        {
            int total = 0;
            foreach (List<int> value in values)
            {
                if (value.Count == 0) continue;
                if (IsValidLine(value)) total += value[value.Count / 2];
            }
            return total.ToString();
        }

        public string? ExecPartB()
        {
            int total = 0;
            List<List<int>> invalidLines = GetInvalidLines();
            foreach (List<int> invalidLine in invalidLines)
            {
                FixInvalidLine(invalidLine);
                total += invalidLine[invalidLine.Count / 2];
            }

            return total.ToString();
        }

        public void Setup(string[] input, string continuousInput)
        {
            string normalizedInput = continuousInput.Replace("\r\n", "\n");
            string[] parts = normalizedInput.Split("\n\n");
            if (parts.Length != 2) return;

            string[] ruleStrings = parts[0].Split('\n');
            string[] valueStrings = parts[1].Split('\n');

            foreach (string rule in ruleStrings)
            {
                string[] split = rule.Split('|');
                if (split.Length != 2) return;

                int first = int.Parse(split[0]);
                int last = int.Parse(split[1]);

                if (forwardRules.TryGetValue(first, out HashSet<int>? forwardKeys)) forwardKeys.Add(last);
                else
                {
                    forwardRules.Add(first, []);
                    forwardRules[first].Add(last);
                }

                if (backwardRules.TryGetValue(last, out HashSet<int>? backwardKeys)) backwardKeys.Add(first);
                else
                {
                    backwardRules.Add(last, []);
                    backwardRules[last].Add(first);
                }
            }

            values = ParsingUtils.ParseDigitsList(valueStrings);
        }
    }
}