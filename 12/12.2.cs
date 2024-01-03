namespace Day12;

class Puzzle2
{
    public long answer(string[] input)
    {
        long result = 0;
        List<(string, int[])> springs = new List<(string, int[])>();
        foreach (string line in input)
        {
            string[] values = line.Split(" ");
            int[] nums = values[1].Split(",").Select(num => int.Parse(num)).ToArray();
            string newString = values[0] + '?' + values[0] + '?' + values[0] + '?' + values[0] + '?' + values[0];
            int l = nums.Length;
            int[] newNums = new int[l * 5];
            for (int i = 0; i < 5; i++)
            {
                Array.Copy(nums, 0, newNums, l * i, l);
            }
            springs.Add((newString, newNums));
        }

        foreach ((string, int[]) row in springs)
        {
            // Memoization is amazing; this solution would have taken hours (or days) without it but takes 0.1s with it
            Dictionary<(int, int, int, int), long> memo = new Dictionary<(int, int, int, int), long>();
            result += countOptions(row.Item1, row.Item2, 0, -1, 0, row.Item1.Length - (row.Item2.Sum() + row.Item2.Length - 1), memo);
        }
        return result;
    }

    // We'll have to optimize now – no more create-all-options-and-check-them.
    // completedCountsIndex tracks the most index of the most recent string of broken springs that's been fully completed (reached correct length and followed by period)
    // currentBrokenLength is the length of the current string of #s, up to and including current index
    // extraSpace is how many "extra" periods (periods after periods) the string can have without running out of room to complete the required broken sequences 
    public long countOptions(string s, int[] counts, int currentIndex, int completedCountsIndex, int currentBrokenLength, int extraSpace, Dictionary<(int, int, int, int), long> memo)
    {
        // Short-circuit for memo
        if (memo.ContainsKey((currentIndex, completedCountsIndex, currentBrokenLength, extraSpace)))
        {
            return memo[(currentIndex, completedCountsIndex, currentBrokenLength, extraSpace)];
        }
        // Before recursing, check to make sure the character (or both options, if ?) at currentIndex doesn't violate the counts array
        if (currentIndex < s.Length)
        {
            bool isPeriodValid = currentIndex == 0 || s[currentIndex - 1] == '.' || (completedCountsIndex < counts.Length - 1 && currentBrokenLength == counts[completedCountsIndex + 1]);
            if (extraSpace < 0) isPeriodValid = false;
            bool isHashValid = currentIndex == 0 || (s[currentIndex - 1] == '.' && completedCountsIndex < counts.Length) || (completedCountsIndex < counts.Length - 1 && currentBrokenLength < counts[completedCountsIndex + 1]);

            long periodOption = 0;
            long hashOption = 0;

            if (s[currentIndex] != '#')
            {
                periodOption = isPeriodValid ? countOptions(s[currentIndex] == '.' ? s : s.Substring(0, currentIndex) + '.' + s.Substring(currentIndex + 1), counts, currentIndex + 1, currentIndex > 0 && s[currentIndex - 1] == '#' ? completedCountsIndex + 1 : completedCountsIndex, 0, (currentIndex == 0 || s[currentIndex - 1] == '.') ? extraSpace - 1 : extraSpace, memo) : 0;
            }
            if (s[currentIndex] != '.')
            {
                hashOption = isHashValid ? countOptions(s[currentIndex] == '#' ? s : s.Substring(0, currentIndex) + '#' + s.Substring(currentIndex + 1), counts, currentIndex + 1, completedCountsIndex, currentBrokenLength + 1, extraSpace, memo) : 0;
            }
            long total = periodOption + hashOption;
            memo.Add((currentIndex, completedCountsIndex, currentBrokenLength, extraSpace), total);
            return total;
        }
        // Made it to the end; now check if the solution is valid and return 1 if so
        else
        {
            if (completedCountsIndex == counts.Length - 1 && s[s.Length - 1] == '.' || completedCountsIndex == counts.Length - 2 && currentBrokenLength == counts[counts.Length - 1]) return 1;
            return 0;
        }
    }
}