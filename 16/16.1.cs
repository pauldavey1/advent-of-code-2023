namespace Day16;
public class Beam
{
    public Beam(int r = 0, int c = 0, char dir = 'E')
    {
        R = r;
        C = c;
        Dir = dir;
    }
    public int R { get; set; }
    public int C { get; set; }
    public char Dir { get; set; }
}

public class Puzzle1
{
    public int Answer(char[,] input)
    {
        List<char>[,] tracker = CreateTracker(input);
        List<Beam> beams = new List<Beam>();
        beams.Add(new Beam());
        while (beams.Count > 0)
        {
            MoveBeam(input, tracker, beams);
        }

        return Score(tracker);
    }

    public List<char>[,] CreateTracker(char[,] input)
    {
        int h = input.GetLength(0);
        int w = input.GetLength(1);
        List<char>[,] tracker = new List<char>[h, w];
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                tracker[i, j] = new List<char>();
            }
        }
        return tracker;
    }

    public void MoveBeam(char[,] input, List<char>[,] tracker, List<Beam> beams)
    {
        int h = input.GetLength(0);
        int w = input.GetLength(1);
        Beam b = beams[0];

        // Check for invalidity
        if (b.R < 0 || b.R >= h || b.C < 0 || b.C >= w)
        {
            beams.Remove(b);
            return;
        }

        // Check for repeats and add to tracker
        if (tracker[b.R, b.C].Contains(b.Dir))
        {
            beams.Remove(b);
            return;
        }
        else tracker[b.R, b.C].Add(b.Dir);

        // Move to next location(s)
        char c = input[b.R, b.C];
        if (c == '.')
        {
            switch (b.Dir)
            {
                case 'N':
                    b.R--;
                    break;
                case 'S':
                    b.R++;
                    break;
                case 'E':
                    b.C++;
                    break;
                case 'W':
                    b.C--;
                    break;
            }
            return;
        }
        if (c == '/')
        {
            switch (b.Dir)
            {
                case 'N':
                    b.C++;
                    b.Dir = 'E';
                    break;
                case 'S':
                    b.C--;
                    b.Dir = 'W';
                    break;
                case 'E':
                    b.R--;
                    b.Dir = 'N';
                    break;
                case 'W':
                    b.R++;
                    b.Dir = 'S';
                    break;
            }
            return;
        }
        if (c == '\\')
        {
            switch (b.Dir)
            {
                case 'N':
                    b.C--;
                    b.Dir = 'W';
                    break;
                case 'S':
                    b.C++;
                    b.Dir = 'E';
                    break;
                case 'E':
                    b.R++;
                    b.Dir = 'S';
                    break;
                case 'W':
                    b.R--;
                    b.Dir = 'N';
                    break;
            }
            return;
        }
        if (c == '|')
        {
            switch (b.Dir)
            {
                case 'N':
                    b.R--;
                    break;
                case 'S':
                    b.R++;
                    break;
                case 'E':
                case 'W':
                    beams.Add(new Beam(b.R + 1, b.C, 'S'));
                    b.R--;
                    b.Dir = 'N';
                    break;
            }
            return;
        }
        if (c == '-')
        {
            switch (b.Dir)
            {
                case 'N':
                case 'S':
                    beams.Add(new Beam(b.R, b.C + 1, 'E'));
                    b.C--;
                    b.Dir = 'W';
                    break;
                case 'E':
                    b.C++;
                    break;
                case 'W':
                    b.C--;
                    break;
            }
            return;
        }
    }

    public int Score(List<char>[,] tracker)
    {
        int h = tracker.GetLength(0);
        int w = tracker.GetLength(1);
        int result = 0;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                result += tracker[i, j].Count > 0 ? 1 : 0;
            }
        }
        return result;
    }

}