namespace Day13;

class Puzzle1
{
    public int answer(string[] input)
    {
        List<string[]> patterns = listPatterns(input);

        int result = 0;
        foreach (string[] p in patterns)
        {
            result += findMirrorRow(p);
        }
        return result;
    }

    public List<string[]> listPatterns(string[] input)
    {
        List<int> blanks = new List<int>();
        blanks.Add(-1);
        for (int i = 0; i < input.Length; i++)
        {
            if (String.IsNullOrWhiteSpace(input[i]))
            {
                blanks.Add(i);
            }
        }
        blanks.Add(input.Length);
        List<string[]> patterns = new List<string[]>();
        for (int i = 0; i < blanks.Count - 1; i++)
        {
            patterns.Add(input.Skip(blanks[i] + 1).Take(blanks[i + 1] - blanks[i] - 1).ToArray());
        }
        return patterns;
    }

    public int findMirrorRow(string[] pattern, bool rotated = false)
    {
        int factor = rotated ? 1 : 100;
        // Check from top
        for (int i = 1; i < pattern.Length; i = i + 2)
        {
            if (pattern[i] == pattern[0])
            {
                for (int j = 1; j > 0; j++)
                {
                    if (j == (i + 1) / 2) return factor * j;
                    if (pattern[j] == pattern[i - j]) continue;
                    else break;
                }
            }
        }

        // Check from bottom
        int z = pattern.Length - 1;
        for (int i = z - 1; i > 0; i = i - 2)
        {
            if (pattern[i] == pattern[z])
            {
                for (int j = 1; j > 0; j++)
                {
                    if (j == (z - i + 1) / 2) return factor * (z - j + 1);
                    if (pattern[z - j] == pattern[i + j]) continue;
                    else break;
                }
            }
        }

        // Check left/right
        return findMirrorRow(rotate(pattern), true);
    }

    public string[] rotate(string[] pattern)
    {
        string[] rotatedArray = new string[pattern[0].Length];
        for (int i = 0; i < pattern[0].Length; i++)
        {
            string s = "";
            for (int j = 0; j < pattern.Length; j++)
            {
                s += pattern[j][i];
            }
            rotatedArray[i] = s;
        }
        return rotatedArray;
    }
}