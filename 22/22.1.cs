namespace Day22;

public static class Puzzle1
{
    public static int Answer(string[] input)
    {
        List<BrickPosition> brickPositions = CalculateBrickPositions(input);

        int result = 0;
        // For each brick, check if bricks above all have multiple supports; if so, it can be disintegrated 
        for (int i = 0; i < brickPositions.Count; i++)
        {
            bool canDisintegrate = true;
            foreach (int b in brickPositions[i].BricksAbove)
            {
                if (brickPositions[b].BricksBelow.Count == 1)
                {
                    canDisintegrate = false;
                    break;
                }
            }
            if (canDisintegrate) result++;
        }

        return result;
    }

    public static List<BrickPosition> CalculateBrickPositions(string[] input)
    {
        List<Brick> bricks = new List<Brick>();
        foreach (string line in input)
        {
            bricks.Add(new Brick(line));
        }
        var sortedBricks = bricks.OrderBy(brick => brick.LowestZ).ToList();
        var brickPositions = new List<BrickPosition>();
        for (int i = 0; i < sortedBricks.Count; i++)
        {
            brickPositions.Add(new BrickPosition(i));
        }

        // Create start map
        (int z, int b)[,] map = new (int z, int b)[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                map[i, j] = (0, -1);
            }
        }

        for (int i = 0; i < sortedBricks.Count; i++)
        {
            DropBrick(sortedBricks[i], map, brickPositions, i);
        }
        return brickPositions;
    }

    public static void DropBrick(Brick b, (int z, int b)[,] map, List<BrickPosition> bp, int i)
    {
        // Vertical bricks drop into place
        if (b.Start.Z != b.End.Z)
        {
            int x = b.Start.X;
            int y = b.Start.Y;
            MarkAAboveB(i, map[x, y].b, bp);
            map[x, y] = (map[x, y].z + b.Length, i);
        }

        // Horizontal bricks lie above highest point below them
        else if (b.Start.X != b.End.X)
        {
            int y = b.Start.Y;
            int minX = Math.Min(b.End.X, b.Start.X);
            int previousLevel = -1;
            // Find highest point brick below the brick's landing area
            for (int x = minX; x < minX + b.Length; x++)
            {
                if (map[x, y].z > previousLevel) previousLevel = map[x, y].z;
            }
            // Add brick to top and mark points of support
            for (int x = minX; x < minX + b.Length; x++)
            {
                if (map[x, y].z == previousLevel)
                {
                    MarkAAboveB(i, map[x, y].b, bp);
                }
                map[x, y] = (previousLevel + 1, i);
            }
        }
        else
        {
            int x = b.Start.X;
            int minY = Math.Min(b.End.Y, b.Start.Y);
            int previousLevel = -1;
            // Find highest point brick below the brick's landing area
            for (int y = minY; y < minY + b.Length; y++)
            {
                if (map[x, y].z > previousLevel) previousLevel = map[x, y].z;
            }
            // Add brick to top and mark points of support
            for (int y = minY; y < minY + b.Length; y++)
            {
                if (map[x, y].z == previousLevel)
                {
                    MarkAAboveB(i, map[x, y].b, bp);
                }
                map[x, y] = (previousLevel + 1, i);
            }
        }
    }

    public static void MarkAAboveB(int a, int b, List<BrickPosition> bp)
    {
        if (!bp[a].BricksBelow.Contains(b)) bp[a].BricksBelow.Add(b);
        if (b > -1 && !bp[b].BricksBelow.Contains(a)) bp[b].BricksAbove.Add(a);
    }
}