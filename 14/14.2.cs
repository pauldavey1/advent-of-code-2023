using System.Text;
namespace Day14;

// It would take several days to run 1000000000 cycles.
// Instead, we'll keep track of each spin cycle's end position.
// Once we find a repeat, that'll be a loop and we can calculate positions beyond.

class Puzzle2
{
    Puzzle1 p1 = new Puzzle1();

    public int answer(char[,] a)
    {
        int target = 1000000000;

        int n = a.GetLength(0);

        // Create dictionary of final positions (strings) after <int> cycles
        Dictionary<string, int> d = new Dictionary<string, int>();
        int loopStart = 0;
        int loopLength = 0;
        for (int i = 1; i <= target; i++)
        {
            spinCycle(a, n, d);
            if (isRepeat(a, n, d, i))
            {
                loopStart = d[buildStringFromArray(a, n)];
                loopLength = i - loopStart;
                break;
            }
        }
        int targetIndex = loopStart + (target - loopStart) % loopLength;
        string targetString = d.FirstOrDefault(pair => pair.Value == targetIndex).Key;
        char[,] targetArray = buildArrayFromString(targetString, n);
        return p1.scoreResult(targetArray);
    }

    public void spinCycle(char[,] a, int n, Dictionary<string, int> d)
    {
        rollAllNorth(a, n);
        rollAllWest(a, n);
        rollAllSouth(a, n);
        rollAllEast(a, n);
    }

    public void rollAllNorth(char[,] a, int n)
    {
        for (int j = 0; j < n; j++)
        {
            for (int i = 0; i < n; i++)
            {
                if (a[i, j] == 'O')
                {
                    p1.rollNorth(a, i, j);
                }
            }
        }
    }

    public void rollAllWest(char[,] a, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (a[i, j] == 'O')
                {
                    rollWest(a, i, j);
                }
            }
        }
    }

    public void rollAllSouth(char[,] a, int n)
    {
        for (int j = 0; j < n; j++)
        {
            for (int i = n - 1; i >= 0; i--)
            {
                if (a[i, j] == 'O')
                {
                    rollSouth(a, i, j, n);
                }
            }
        }
    }

    public void rollAllEast(char[,] a, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = n - 1; j >= 0; j--)
            {
                if (a[i, j] == 'O')
                {
                    rollEast(a, i, j, n);
                }
            }
        }
    }

    void rollWest(char[,] a, int i, int j)
    {
        while (j > 0 && a[i, j - 1] == '.')
        {
            a[i, j] = '.';
            a[i, --j] = 'O';
        }
    }

    void rollSouth(char[,] a, int i, int j, int n)
    {
        while (i < n - 1 && a[i + 1, j] == '.')
        {
            a[i, j] = '.';
            a[++i, j] = 'O';
        }
    }

    void rollEast(char[,] a, int i, int j, int n)
    {
        while (j < n - 1 && a[i, j + 1] == '.')
        {
            a[i, j] = '.';
            a[i, ++j] = 'O';
        }
    }

    bool isRepeat(char[,] a, int n, Dictionary<string, int> d, int x)
    {
        return !d.TryAdd(buildStringFromArray(a, n), x);
    }

    string buildStringFromArray(char[,] a, int n)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sb.Append(a[i, j]);
            }
        }
        return sb.ToString();
    }

    char[,] buildArrayFromString(string s, int n)
    {
        char[,] result = new char[n, n];
        int i = 0;
        int j = 0;
        foreach (char c in s)
        {
            result[i, j++] = c;
            if (j == n)
            {
                i++;
                j = 0;
            }
        }
        return result;
    }
}