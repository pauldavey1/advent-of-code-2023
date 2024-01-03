// Read input to create array of strings
string[] input = File.ReadAllLines("input.txt");

(int, string)[] dict = { (1, "1"), (2, "2"), (3, "3"), (4, "4"), (5, "5"), (6, "6"), (7, "7"), (8, "8"), (9, "9"), (1, "one"), (2, "two"), (3, "three"), (4, "four"), (5, "five"), (6, "six"), (7, "seven"), (8, "eight"), (9, "nine") };

int result = 0;

// For each list value, get 2-digit number and add to solution
foreach (string str in input)
{
    int firstIndex = 1000;
    int firstValue = -1;
    int lastIndex = -1;
    int lastValue = -1;

    foreach ((int i, string s) in dict)
    {
        int newFirstIndex = str.IndexOf(s);
        int newLastIndex = str.LastIndexOf(s);
        if (newFirstIndex > -1 && newFirstIndex < firstIndex)
        {
            firstIndex = newFirstIndex;
            firstValue = i;
        }
        if (newLastIndex > lastIndex)
        {
            lastIndex = newLastIndex;
            lastValue = i;
        }
    }

    int output = 10 * firstValue + lastValue;
    result += output;
}

Console.WriteLine(result);