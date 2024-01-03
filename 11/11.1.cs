using System.Text;
namespace Day11;
class Puzzle1
{
    public int answer(List<string> input)
    {
        int h = input.Count;
        int w = input[0].Length;

        // Add blank rows
        for (int i = 0; i < h; i++)
        {
            if (!input[i].Contains('#'))
            {
                input.Insert(i, new string('.', w));
                i++;
            }
        }
        h = input.Count;

        // Add blank columns
        List<int> blankColumns = new List<int>();
        for (int j = 0; j < w; j++)
        {
            bool empty = true;
            for (int i = 0; i < h; i++)
            {
                if (input[i][j] == '#')
                {
                    empty = false;
                    break;
                }
            }
            if (empty) blankColumns.Add(j);
        }

        w += blankColumns.Count;

        for (int i = 0; i < h; i++)
        {
            StringBuilder sb = new StringBuilder(input[i]);
            for (int k = blankColumns.Count - 1; k > -1; k--)
            {
                sb.Insert(blankColumns[k], ".");
            }
            input[i] = sb.ToString();
        }

        // Create list of galaxy coordinates
        List<(int r, int c)> galaxies = new List<(int r, int c)>();
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                if (input[i][j] == '#')
                {
                    galaxies.Add((i, j));
                }
            }
        }

        // Add up pair distances
        int result = 0;
        for (int a = 0; a < galaxies.Count - 1; a++)
        {
            for (int b = a + 1; b < galaxies.Count; b++)
            {
                result += Math.Abs(galaxies[a].r - galaxies[b].r) + Math.Abs(galaxies[a].c - galaxies[b].c);
            }
        }

        return result;
    }
}