string[] input = File.ReadAllLines("input.txt");
int[] times = new int[4];
int[] distances = new int[4];

foreach (string line in input)
{
    int colonIndex = line.IndexOf(':');
    string[] nums = line.Substring(colonIndex + 1).Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for (int i = 0; i < nums.Length; i++)
    {
        if (line[0] == 'T')
        {

            times[i] = int.Parse(nums[i]);
        }
        else
        {
            distances[i] = int.Parse(nums[i]);
        }
    }
}

long result = 1;
for (int i = 0; i < times.Length; i++)
{
    int t = times[i];
    int d = distances[i];
    // Find shortest hold time that works
    long minWin = 0;
    for (int x = 1; x < t; x++)
    {
        if (x * (t - x) > d)
        {
            minWin = x;
            break;
        }
    }

    // Find longest hold time that works
    long maxWin = 0;
    for (int x = t - 1; x > 0; x--)
    {
        if (x * (t - x) > d)
        {
            maxWin = x;
            break;
        }
    }

    // Find number of solutions
    result *= maxWin - minWin + 1;
}

Console.WriteLine(result);