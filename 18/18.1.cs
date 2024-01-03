namespace Day18;

class Puzzle1
{
    public int Answer(List<Instruction> input)
    {
        (int H, int W, int R, int C) gridProps = FindGridProps(input);
        char[,] grid = CreateGrid(input, gridProps);
        FillGrid(grid);
        return CountGrid(grid);
    }

    public (int, int, int, int) FindGridProps(List<Instruction> input)
    {
        // Find the furthest distance from the starting point in each direction
        int maxU = 0, maxD = 0, maxL = 0, maxR = 0, currU = 0, currL = 0;
        foreach (Instruction inst in input)
        {
            switch (inst.Direction)
            {
                case "U":
                    currU += inst.Count;
                    if (currU > maxU) maxU = currU;
                    break;
                case "D":
                    currU -= inst.Count;
                    if (-currU > maxD) maxD = -currU;
                    break;
                case "L":
                    currL += inst.Count;
                    if (currL > maxL) maxL = currL;
                    break;
                case "R":
                    currL -= inst.Count;
                    if (-currL > maxR) maxR = -currL;
                    break;
            }
        }


        // Return the height, width, and start position for a grid that will contain the full loop
        return (maxU + maxD + 1, maxL + maxR + 1, maxU, maxL);
    }
    public char[,] CreateGrid(List<Instruction> input, (int H, int W, int R, int C) gridProps)
    {
        // Create grid of periods
        char[,] grid = new char[gridProps.H, gridProps.W];
        for (int i = 0; i < gridProps.H; i++)
        {
            for (int j = 0; j < gridProps.W; j++)
            {
                grid[i, j] = '.';
            }
        }

        // Add loop to grid
        int currR = gridProps.R, currC = gridProps.C;
        foreach (Instruction inst in input)
        {
            for (int i = 0; i < inst.Count; i++)
            {
                switch (inst.Direction)
                {
                    // Need to ensure up and down are marked all along the trench for finding inside
                    case "U":
                        grid[currR, currC] = 'U';
                        grid[--currR, currC] = 'U';
                        break;
                    case "D":
                        grid[currR, currC] = 'D';
                        grid[++currR, currC] = 'D';
                        break;
                    case "L":
                        grid[currR, --currC] = '#';
                        break;
                    case "R":
                        grid[currR, ++currC] = '#';
                        break;
                }
            }
        }
        return grid;
    }
    public void FillGrid(char[,] grid)
    {
        // Depending on whether U or D is encountered first, can determine whether loop is CW or CCW
        char first = '?';
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            bool inside = false;
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                switch (grid[i, j])
                {
                    case '#':
                        break;
                    case 'U':
                        if (first == '?') first = 'U';
                        inside = first == 'U';
                        grid[i, j] = '#';
                        break;
                    case 'D':
                        if (first == '?') first = 'D';
                        inside = first == 'D';
                        grid[i, j] = '#';
                        break;
                    case '.':
                        if (inside) grid[i, j] = '#';
                        break;
                }
            }
        }
    }

    public int CountGrid(char[,] grid)
    {
        int result = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == '#') result++;
            }
        }
        return result;
    }
}

class Instruction
{
    public string Direction { get; set; }
    public int Count { get; set; }
    public string Code { get; set; }
    public Instruction(string[] arr)
    {
        Direction = arr[0];
        Count = int.Parse(arr[1]);
        Code = arr[2].Substring(2, 6);
    }
}