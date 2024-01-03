using System.Text;
namespace Day11;
class Puzzle2
{
    public long answer(List<string> input)
    {
        int h = input.Count;
        int w = input[0].Length;

        // Mark blank rows
        for (int i = 0; i < h; i++)
        {
            if (!input[i].Contains('#'))
            {
                input[i] = new string('0', w);
            }
        }

        // Mark blank columns
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

        for (int i = 0; i < h; i++)
        {
            StringBuilder sb = new StringBuilder(input[i]);
            for (int k = blankColumns.Count - 1; k > -1; k--)
            {
                sb[blankColumns[k]] = '0';
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

        // Add up pair distances by traversing paths
        long result = 0;
        for (int a = 0; a < galaxies.Count - 1; a++)
        {
            for (int b = a + 1; b < galaxies.Count; b++)
            {
                int r1 = galaxies[a].r;
                int r2 = galaxies[b].r;
                int c1 = galaxies[a].c;
                int c2 = galaxies[b].c;
                while (r1 != r2)
                {
                    if (r1 < r2) result += input[++r1][c1] == '0' ? 1000000 : 1;
                    else result += input[--r1][c1] == '0' ? 1000000 : 1;
                }
                while (c1 != c2)
                {
                    if (c1 < c2) result += input[r1][++c1] == '0' ? 1000000 : 1;
                    else result += input[r1][--c1] == '0' ? 1000000 : 1;
                }
            }
        }

        return result;
    }
}