namespace Day15;

class Puzzle1
{
    public int answer(string[] input)
    {
        int result = 0;
        foreach (string s in input)
        {
            result += hash(s);
        }
        return result;

    }

    public int hash(string s)
    {
        int current = 0;
        foreach (char c in s)
        {
            current += (int)c;
            current *= 17;
            current %= 256;
        }
        return current;
    }
}