string[] input = File.ReadAllLines("input.txt");

int result = 0;

// Add border of periods around input to unify border checks
string[] borderedInput = new string[input.Length + 2];
for (int i = 0; i < input.Length; i++)
{
    borderedInput[i + 1] = '.' + input[i] + '.';
}
borderedInput[0] = new string('.', borderedInput[1].Length);
borderedInput[borderedInput.Length - 1] = new string('.', borderedInput[1].Length);

int parseNumber(int r, int i)
{
    int start = i;
    int length = 1;
    while (char.IsDigit(borderedInput[r][start - 1]))
    {
        start--;
        length++;
    }
    while (char.IsDigit(borderedInput[r][start + length]))
    {
        length++;
    }
    return int.Parse(borderedInput[r].Substring(start, length));
}

// Check numbers around asterisk
int gearRatio(int r, int i)
{
    char a = borderedInput[r - 1][i - 1], b = borderedInput[r - 1][i], c = borderedInput[r - 1][i + 1], d = borderedInput[r][i - 1], e = borderedInput[r][i + 1], f = borderedInput[r + 1][i - 1], g = borderedInput[r + 1][i], h = borderedInput[r + 1][i + 1];
    List<int> gearNumbers = new List<int>();
    if (char.IsDigit(a))
    {
        gearNumbers.Add(parseNumber(r - 1, i - 1));
    }
    if (!char.IsDigit(a) && char.IsDigit(b))
    {
        gearNumbers.Add(parseNumber(r - 1, i));
    }
    if (!char.IsDigit(b) && char.IsDigit(c))
    {
        gearNumbers.Add(parseNumber(r - 1, i + 1));
    }
    if (char.IsDigit(d))
    {
        gearNumbers.Add(parseNumber(r, i - 1));
    }
    if (char.IsDigit(e))
    {
        gearNumbers.Add(parseNumber(r, i + 1));
    }
    if (char.IsDigit(f))
    {
        gearNumbers.Add(parseNumber(r + 1, i - 1));
    }
    if (!char.IsDigit(f) && char.IsDigit(g))
    {
        gearNumbers.Add(parseNumber(r + 1, i));
    }
    if (!char.IsDigit(g) && char.IsDigit(h))
    {
        gearNumbers.Add(parseNumber(r + 1, i + 1));
    }
    if (gearNumbers.Count == 2)
    {
        return gearNumbers[0] * gearNumbers[1];
    }
    return 0;
}

for (int r = 1; r < borderedInput.Length - 1; r++)
{
    string line = borderedInput[r];
    int i = 1;
    while (i < line.Length - 1)
    {
        if (line[i] == '*')
        {
            result += gearRatio(r, i);
        }
        i++;
    }
}

Console.WriteLine(result);