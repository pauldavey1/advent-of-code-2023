string[] input = File.ReadAllLines("input.txt");
string instructions = input[0];
Dictionary<string, Map> maps = new Dictionary<string, Map>();

for (int i = 2; i < input.Length; i++)
{
    maps[input[i].Substring(0, 3)] = new Map(input[i]);
}

int count = 0;
int index = 0;
string current = "AAA";

while (current != "ZZZ")
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
}

Console.WriteLine(count);

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