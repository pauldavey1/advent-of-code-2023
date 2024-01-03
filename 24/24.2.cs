namespace Day24;

public static class Puzzle2
{
    public static long Answer(string[] input)
    {
        var hailstones = new List<Hailstone>();
        foreach (string line in input)
        {
            hailstones.Add(new Hailstone(line));
        }

        // Assumption: Rock's speed in each dimension will be in the range -1000 to +1000, like hailstones
        // Code that follows is tailored to the input and would not work if the input were different

        // For each dimension, some hailstones have matching velocities
        // Since these stones are moving at the same rate, the rock's velocity must be related to that rate:
        // If (pHailA-pHailB)%(vRock-vHail) != 0, the rock won't hit both hailstones
        // So in each dimension, we'll rule out invalid values of vRock

        // Create lists of possible velocities
        var vxOptions = Enumerable.Range(-1000, 2001).ToList();
        var vyOptions = Enumerable.Range(-1000, 2001).ToList();
        var vzOptions = Enumerable.Range(-1000, 2001).ToList();

        // Create lists of matching pairs (hailstones with same velocity in specified dimension)
        var xMatches = new List<(long, long, long)>();
        var yMatches = new List<(long, long, long)>();
        var zMatches = new List<(long, long, long)>();
        for (int i = 0; i < hailstones.Count - 1; i++)
        {
            for (int j = i + 1; j < hailstones.Count; j++)
            {
                if (hailstones[i].Vx == hailstones[j].Vx) xMatches.Add((hailstones[i].Px, hailstones[j].Px, hailstones[i].Vx));
                if (hailstones[i].Vy == hailstones[j].Vy) yMatches.Add((hailstones[i].Py, hailstones[j].Py, hailstones[i].Vy));
                if (hailstones[i].Vz == hailstones[j].Vz) zMatches.Add((hailstones[i].Pz, hailstones[j].Pz, hailstones[i].Vz));
            }
        }

        // For each pair, rule out velocity options that wouldn't allow the rock to hit both stones
        FilterOptions(xMatches, vxOptions);
        FilterOptions(yMatches, vyOptions);
        FilterOptions(zMatches, vzOptions);

        // End up with just one possibility for the rock's velocity: vx = 192, vy = 210, vz = 179
        long vx = vxOptions[0];
        long vy = vyOptions[0];
        long vz = vzOptions[0];
        long px = 0;
        long py = 0;
        long pz = 0;

        // For x & y, there are hailstones with the same velocity as the rock, so the rock's px & py must match those if it's going to collide at any point
        foreach (Hailstone h in hailstones)
        {
            if (h.Vx == vx) px = h.Px;
            if (h.Vy == vy) py = h.Py;
        }
        // For z, should be able to calculate rock's pz based on the time it takes to collide with one of the known hailstones
        foreach (Hailstone h in hailstones)
        {
            if (h.Px == px)
            {
                long time = (h.Py - 175507140888229) / (210 - h.Vy);
                pz = h.Pz + time * (h.Vz - vz);
            }
        }

        return px + py + pz;
    }

    public static void FilterOptions(List<(long, long, long)> matches, List<int> options)
    {
        foreach ((long pa, long pb, long vHail) match in matches)
        {
            for (int i = options.Count - 1; i >= 0; i--)
            {
                if (options[i] == match.vHail && match.pa != match.pb)
                {
                    options.Remove(options[i]);
                }
                if ((match.pa - match.pb) % (options[i] - match.vHail) != 0)
                {
                    options.Remove(options[i]);
                }
            }
        }
    }
}