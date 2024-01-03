namespace Day14;

class Puzzle1
{
    public int answer(char[,] a)
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                if (a[i, j] == 'O') rollNorth(a, i, j);
            }
        }
        return scoreResult(a);
    }

    public void rollNorth(char[,] a, int i, int j)
    {
        while (i > 0 && a[i - 1, j] == '.')
        {
            a[i, j] = '.';
            a[--i, j] = 'O';
        }
    }

    public int scoreResult(char[,] a)
    {
        int result = 0;
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                if (a[i, j] == 'O') result += a.GetLength(0) - i;
            }
        }
        return result;
    }
}