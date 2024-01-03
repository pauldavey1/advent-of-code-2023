namespace Day24;

public static class Puzzle1
{
    public static int Answer(string[] input)
    {
        var hailstones = new List<Hailstone>();
        foreach (string line in input)
        {
            hailstones.Add(new Hailstone(line));
        }

        int result = 0;
        for (int i = 0; i < hailstones.Count - 1; i++)
        {
            for (int j = i + 1; j < hailstones.Count; j++)
            {
                result += CheckMatch(hailstones[i], hailstones[j]);
            }
        }

        return result;
    }

    public static int CheckMatch(Hailstone a, Hailstone b)
    {
        long lowerBound = 200000000000000;
        long upperBound = 400000000000000;
        // If lines are parallel, they won't meet
        if (a.XYSlope == b.XYSlope) return 0;
        double x = (b.YIntercept - a.YIntercept) / (a.XYSlope - b.XYSlope);
        double y = a.XYSlope * x + a.YIntercept;
        // Check if intersection is outside of test area
        if (x < lowerBound || x > upperBound || y < lowerBound || y > upperBound) return 0;
        // Check if intersection is in the past for either hailstone
        if (((x - a.Px) * a.Vx < 0) || ((x - b.Px) * b.Vx < 0)) return 0;
        return 1;
    }
}