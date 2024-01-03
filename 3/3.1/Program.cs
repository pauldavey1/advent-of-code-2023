string[] input = File.ReadAllLines("input.txt");

int result = 0;

// Add border of periods around input to unify border checks for interior numbers
string[] borderedInput = new string[input.Length + 2];
for (int i = 0; i < input.Length; i++)
{
    borderedInput[i + 1] = '.' + input[i] + '.';
}
borderedInput[0] = new string('.', borderedInput[1].Length);
borderedInput[borderedInput.Length - 1] = new string('.', borderedInput[1].Length);

// Check characters around a number for an adjacent symbol
bool isPart(int r, int i, int numLength)
{
    string borderString = borderedInput[r - 1].Substring(i - 1, numLength + 2) + borderedInput[r][i - 1] + borderedInput[r][i + numLength] + borderedInput[r + 1].Substring(i - 1, numLength + 2);
    foreach (char c in borderString)
    {
        if (!char.IsDigit(c) && c != '.')
        {
            return true;
        }
    }
    return false;
}

for (int r = 1; r < borderedInput.Length - 1; r++)
{
    string line = borderedInput[r];
    int i = 1;
    while (i < line.Length - 1)
    {
        if (char.IsDigit(line[i]))
        {
            int numLength = 1;
            while (char.IsDigit(line[i + numLength]))
            {
                numLength++;
            }
            if (isPart(r, i, numLength))
            {
                result += int.Parse(borderedInput[r].Substring(i, numLength));
            }
            i += numLength;
            continue;
        }
        i++;
    }
}

Console.WriteLine(result);