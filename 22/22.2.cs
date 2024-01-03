namespace Day22;

public static class Puzzle2
{
    public static int Answer(string[] input)
    {
        List<BrickPosition> brickPositions = Puzzle1.CalculateBrickPositions(input);

        int result = 0;
        for (int i = 0; i < brickPositions.Count; i++)
        {
            result += CheckAbove(i, brickPositions);
        }
        return result;
    }

    public static int CheckAbove(int i, List<BrickPosition> bp)
    {
        var unstableBricks = new List<int>();
        var bricksToCheck = new List<int>();
        unstableBricks.Add(i);
        bricksToCheck.Add(i);
        while (bricksToCheck.Count > 0)
        {
            int current = bricksToCheck[0];
            // Check all bricks above for instability
            foreach (int b in bp[current].BricksAbove)
            {
                bool isUnstable = true;
                // If supported by any stable bricks, brick is stable
                foreach (int c in bp[b].BricksBelow)
                {
                    if (!unstableBricks.Contains(c)) isUnstable = false;
                }
                // If unstable, check add to list and check bricks above
                if (isUnstable)
                {
                    if (!unstableBricks.Contains(b))
                    {
                        unstableBricks.Add(b);
                        bricksToCheck.Add(b);
                    }
                }
            }
            bricksToCheck.RemoveAt(0);
        }
        return unstableBricks.Count - 1;
    }
}
