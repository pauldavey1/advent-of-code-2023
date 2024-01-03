int[][] input = File.ReadAllLines("input.txt").Select(line => line.Split(' ').Select(num => int.Parse(num)).ToArray()).ToArray();

int result = 0;

foreach (int[] line in input)
{
    int[] current = (line);
    // Just reverse the array and run the same code
    Array.Reverse(current);
    int lineResult = 0;
    bool done = false;
    while (!done)
    {
        lineResult += current[current.Length - 1];
        int[] next = calculateDifferences(current);

        done = true;
        foreach (int i in next)
        {
            if (i != 0) done = false;
        }

        current = next;
    }
    result += lineResult;
}

Console.WriteLine(result);

int[] calculateDifferences(int[] input)
{
    int[] output = new int[input.Length - 1];
    for (int i = 0; i < input.Length - 1; i++)
    {
        output[i] = input[i + 1] - input[i];
    }
    return output;
}