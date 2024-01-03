Console.WriteLine("Hello, World! I'm learning C#!");

// Read input to create array of strings
string[] input = File.ReadAllLines("input.txt");

int result = 0;

// For each list value, get 2-digit number and add to solution
foreach (string str in input)
{
    int firstIndex = -1;
    int lastIndex = -1;
    int searchIndex = 0;
    while (firstIndex == -1)
    {
        if (char.IsDigit(str[searchIndex]))
        {
            firstIndex = searchIndex;
        }
        searchIndex++;
    }
    searchIndex = str.Length - 1;
    while (lastIndex == -1)
    {
        if (char.IsDigit(str[searchIndex]))
        {
            lastIndex = searchIndex;
        }
        searchIndex--;
    }

    int a = int.Parse(str[firstIndex].ToString());
    int b = int.Parse(str[lastIndex].ToString());

    int output = 10 * a + b;
    result += output;
}

Console.WriteLine(result);