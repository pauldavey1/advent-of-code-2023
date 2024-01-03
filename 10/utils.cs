namespace Day10;


class SharedUtils
{
    public (char, int, int) FindNext(string[] input, (char, int, int) loc)
    {
        (char dir, int r, int c) = loc;
        char pipe = input[r][c];
        if (dir == 'N')
        {
            if (pipe == '|')
            {
                return ('N', r - 1, c);
            }
            else if (pipe == '7')
            {
                return ('W', r, c - 1);
            }
            else if (pipe == 'F')
            {
                return ('E', r, c + 1);
            }
        }
        if (dir == 'S')
        {
            if (pipe == '|')
            {
                return ('S', r + 1, c);
            }
            else if (pipe == 'J')
            {
                return ('W', r, c - 1);
            }
            else if (pipe == 'L')
            {
                return ('E', r, c + 1);
            }
        }
        if (dir == 'W')
        {
            if (pipe == '-')
            {
                return ('W', r, c - 1);
            }
            else if (pipe == 'L')
            {
                return ('N', r - 1, c);
            }
            else if (pipe == 'F')
            {
                return ('S', r + 1, c);
            }
        }
        if (dir == 'E')
        {
            if (pipe == '-')
            {
                return ('E', r, c + 1);
            }
            else if (pipe == 'J')
            {
                return ('N', r - 1, c);
            }
            else if (pipe == '7')
            {
                return ('S', r + 1, c);
            }
        }
        throw new ArgumentException("Invalid pipe path.");
    }
}