string[] input = File.ReadAllLines("input.txt");
string instructions = input[0];
Dictionary<string, Map> maps = new Dictionary<string, Map>();

for (int i = 2; i < input.Length; i++)
{
    maps[input[i].Substring(0, 3)] = new Map(input[i]);
}

string[] startNodes = maps.Where(pair => pair.Key[2] == 'A').Select(pair => pair.Key).ToArray();

// Find each start value's pattern
foreach (string s in startNodes)
{
    int count = 0;
    int index = 0;
    string current = s;

    while (count < 200000)
    {
        if (instructions[index] == 'L')
        {
            current = maps[current].left;
        }
        else
        {
            current = maps[current].right;
        }
        if (++index == instructions.Length) index = 0;
        count++;
        // if (current[2] == 'Z') Console.WriteLine(count);
    }

    // Console.WriteLine("—————");
}

// This reveals that the maps cycle in loops
// Need to find least common multiple of those loops

int[] loops = { 18727, 24253, 18113, 22411, 21797, 16271 };
Dictionary<int, int> lcmFactors = new Dictionary<int, int>();

foreach (int loopNum in loops)
{
    Dictionary<int, int> factors = new Dictionary<int, int>();
    int i = 2;
    int x = loopNum;
    while (i * i < x)
    {
        if (x % i == 0)
        {
            if (factors.ContainsKey(i)) factors[i]++;
            else factors[i] = 1;
            x /= i;
        }
        else
        {
            i++;
        }
    }

    // Add remainder (which could already be in the dictionary, if the largest factor is squared)
    if (factors.ContainsKey(x)) factors[x]++;
    else factors[x] = 1;

    // Since each loopNum has just two factors, it's easier to multiply by hand, but for the sake of argument:
    foreach ((int f, int count) in factors)
    {
        if (!lcmFactors.ContainsKey(f)) lcmFactors[f] = 1;
        else if (lcmFactors[f] < count) lcmFactors[f] = count;
    }
}

// Multiply all factors together for result
long result = 1;
foreach ((int f, int count) in lcmFactors)
{
    for (int i = 0; i < count; i++)
    {
        result *= f;
    }
}

Console.WriteLine(result);

class Map
{
    public string left;
    public string right;

    public Map(string line)
    {
        left = line.Substring(7, 3);
        right = line.Substring(12, 3);
    }
}