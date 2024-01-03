namespace Day13;

class Puzzle2
{
    Puzzle1 p1 = new Puzzle1();
    public int answer(string[] input)
    {
        List<string[]> patterns = p1.listPatterns(input);

        int result = 0;
        foreach (string[] p in patterns)
        {
            result += findSmudgedMirrorRow(p);
        }
        return result;
    }

    public int findSmudgedMirrorRow(string[] pattern, bool rotated = false)
    {
        int factor = rotated ? 1 : 100;

        // The input is small enough that a brute-force solution seems fine.
        // We'll check every possible mirror position and find one with exactly one smudge.
        for (int i = 1; i < pattern.Length; i++)
        {
            // Calculate number of rows to check
            int n = Math.Min(i, pattern.Length - i);
            string above = "";
            string below = "";
            for (int m = 1; m <= n; m++)
            {
                above += pattern[i - m];
                below += pattern[i + m - 1];
            }

            int smudges = 0;
            for (int j = 0; j < above.Length; j++)
            {
                if (above[j] != below[j]) smudges++;
                if (smudges > 1) break;
            }
            if (smudges == 1) return factor * i;
        }
        return findSmudgedMirrorRow(p1.rotate(pattern), true);
    }
}