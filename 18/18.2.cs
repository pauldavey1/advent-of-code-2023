namespace Day18;

class Puzzle2
{
    Puzzle1 p1 = new Puzzle1();

    public long Answer(List<Instruction> input)
    {
        foreach (Instruction inst in input)
        {
            inst.Count = int.Parse(inst.Code.Substring(0, 5), System.Globalization.NumberStyles.HexNumber);
            switch (inst.Code[5])
            {
                case '0':
                    inst.Direction = "R"; break;
                case '1':
                    inst.Direction = "D"; break;
                case '2':
                    inst.Direction = "L"; break;
                case '3':
                    inst.Direction = "U"; break;
            };
        }
        (int H, int W, int R, int C) gridProps = p1.FindGridProps(input);
        long result = 0;
        for (int i = 0; i < gridProps.H; i++)
        {
            result += CountRow(CreateRow(input, gridProps, i));
        }
        return result;
    }

    public SortedDictionary<int, string> CreateRow(List<Instruction> input, (int H, int W, int R, int C) gridProps, int rowNum)
    {
        // Create dictionary of loop edges within row
        SortedDictionary<int, string> row = new SortedDictionary<int, string>();

        int currR = gridProps.R, currC = gridProps.C;

        foreach (Instruction inst in input)
        {
            switch (inst.Direction)
            {
                // Like in puzzle 1, save U/D all along the edge, including both corners
                case "U":
                    if (rowNum <= currR && rowNum >= currR - inst.Count) row.Add(currC, "U");
                    currR -= inst.Count;
                    break;
                case "D":
                    if (rowNum >= currR && rowNum <= currR + inst.Count) row.Add(currC, "D");
                    currR += inst.Count;
                    break;
                // For L/R edges, save the length of the sequence between the U & D (inst.Count - 1)
                case "L":
                    if (rowNum == currR && currR == rowNum) row.Add(currC - 1, (inst.Count - 1).ToString());
                    currC -= inst.Count;
                    break;
                case "R":
                    if (rowNum == currR && currR == rowNum) row.Add(currC + 1, (inst.Count - 1).ToString());
                    currC += inst.Count;
                    break;
            }
        }

        return row;
    }

    // This got messy.
    public int CountRow(SortedDictionary<int, string> row, bool log = false)
    {
        int result = 0;
        string first = "?";
        int lastIn = -1;
        // Tracks whether we're on an edge.
        // Needed to differentiate between URRRD and U...D
        bool onEdge = false;
        foreach (KeyValuePair<int, string> pair in row)
        {
            // Save which value comes first to determine CW/CCW
            if (first == "?") first = pair.Value;
            if (pair.Value == first)
            {
                // Add the square that contains the U/D
                result++;
                // Mark that we're inside now
                lastIn = pair.Key;
                onEdge = false;
            }
            else if (pair.Value == "U" || pair.Value == "D")
            {
                // Add the square that contains the U/D
                result++;
                // If we're exiting the inside, need to add the inside area and reset lastIn
                if (lastIn != -1)
                {
                    // We want to add 3 for U...D, but if it's URRRD, the RRR has already been added via the else clause, so we should skip it here
                    if (!onEdge) result += pair.Key - lastIn - 1;
                    lastIn = -1;
                }
                onEdge = false;
            }
            else
            {
                // Add the length of the RRR/LLL between U & D
                result += int.Parse(pair.Value);
                // Note that we're on an edge
                onEdge = true;
            }
        }
        return result;
    }
}