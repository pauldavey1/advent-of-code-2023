string[] input = File.ReadAllLines("input.txt");
long t = 0;
long d = 0;

foreach (string line in input)
{
    int colonIndex = line.IndexOf(':');
    string num = line.Substring(colonIndex + 1).Replace(" ", "");
    if (line[0] == 'T')
    {
        t = long.Parse(num);
    }
    else
    {
        d = long.Parse(num);
    }
}

// Find shortest hold time that works
long minWin = 0;
for (long x = 1; x < t; x++)
{
    if (x * (t - x) > d)
    {
        minWin = x;
        break;
    }
}

// Find longest hold time that works
long maxWin = 0;
for (long x = t - 1; x > 0; x--)
{
    if (x * (t - x) > d)
    {
        maxWin = x;
        break;
    }
}

// Find number of solutions
long result = maxWin - minWin + 1;

Console.WriteLine(result);