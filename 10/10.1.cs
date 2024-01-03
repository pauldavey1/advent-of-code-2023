namespace Day10;

class Puzzle1
{
    public int answer(string[] input)
    {
        (int r, int c) start = (-1, -1);
        int i = 0;
        while (start.r == -1)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'S')
                {
                    start = (i, j);
                    break;
                }
            }
            i++;
        }
        List<(char dir, int r, int c)> current = new List<(char dir, int r, int c)>();
        if (input[start.r - 1][start.c] == '|' || input[start.r - 1][start.c] == '7' || input[start.r - 1][start.c] == 'F') current.Add(('N', start.r - 1, start.c));
        if (input[start.r + 1][start.c] == '|' || input[start.r + 1][start.c] == 'J' || input[start.r + 1][start.c] == 'L') current.Add(('S', start.r + 1, start.c));
        if (input[start.r][start.c - 1] == '-' || input[start.r][start.c - 1] == 'L' || input[start.r][start.c - 1] == 'F') current.Add(('W', start.r, start.c - 1));
        if (input[start.r][start.c + 1] == '-' || input[start.r][start.c + 1] == 'J' || input[start.r][start.c + 1] == '7') current.Add(('E', start.r, start.c + 1));

        int count = 1;
        SharedUtils utils = new SharedUtils();
        while (true)
        {
            List<(char dir, int r, int c)> newCoordinates = new List<(char dir, int r, int c)>();
            foreach ((char dir, int r, int c) in current)
            {
                newCoordinates.Add(utils.FindNext(input, (dir, r, c)));
            }
            count++;
            for (int a = 0; a < newCoordinates.Count - 1; a++)
            {
                for (int b = a + 1; b < newCoordinates.Count; b++)
                {
                    if (newCoordinates[a].r == newCoordinates[b].r && newCoordinates[a].c == newCoordinates[b].c) return count;
                }
            }
            current = newCoordinates;
        }
    }
}