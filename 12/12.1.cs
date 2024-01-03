namespace Day12;

class Puzzle1
{
    public int answer(string[] input)
    {
        int result = 0;
        List<(string, int[])> springs = new List<(string, int[])>();
        foreach (string line in input)
        {
            string[] values = line.Split(" ");
            int[] nums = values[1].Split(",").Select(num => int.Parse(num)).ToArray();
            springs.Add((values[0], nums));
        }

        // There are probably all sorts of optimizations you could use here.
        // But I think it's a small enough problem that brute-force recursion is fine.

        foreach ((string, int[]) row in springs)
        {
            result += countOptions(row.Item1, row.Item2);
        }
        return result;
    }
    public int countOptions(string s, int[] counts)
    {
        int q = s.IndexOf('?');
        // If ?s remain, run recursively on both possibilities for the first ?
        if (q > -1)
        {
            return countOptions(s.Substring(0, q) + '.' + s.Substring(q + 1), counts) + countOptions(s.Substring(0, q) + '#' + s.Substring(q + 1), counts);
        }

        // If ?s are gone, check if string is valid
        else
        {
            string[] brokenStrings = s.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (brokenStrings.Length != counts.Length) return 0;
            for (int i = 0; i < brokenStrings.Length; i++)
            {
                if (brokenStrings[i].Length != counts[i]) return 0;
            }
            return 1;
        }
    }
}