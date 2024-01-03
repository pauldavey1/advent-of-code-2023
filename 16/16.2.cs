namespace Day16;

public class Puzzle2
{
    Puzzle1 p1 = new Puzzle1();
    public int Answer(char[,] input)
    {
        int h = input.GetLength(0);
        int w = input.GetLength(1);
        int maxResult = 0;

        for (int i = 0; i < w; i++)
        {
            List<char>[,] tracker = p1.CreateTracker(input);
            List<Beam> beams = new List<Beam>();
            beams.Add(new Beam(0, i, 'S'));
            while (beams.Count > 0)
            {
                p1.MoveBeam(input, tracker, beams);
            }
            int result = p1.Score(tracker);
            if (result > maxResult) maxResult = result;
        }
        for (int i = 0; i < w; i++)
        {
            List<char>[,] tracker = p1.CreateTracker(input);
            List<Beam> beams = new List<Beam>();
            beams.Add(new Beam(h - 1, i, 'N'));
            while (beams.Count > 0)
            {
                p1.MoveBeam(input, tracker, beams);
            }
            int result = p1.Score(tracker);
            if (result > maxResult) maxResult = result;
        }
        for (int i = 0; i < h; i++)
        {
            List<char>[,] tracker = p1.CreateTracker(input);
            List<Beam> beams = new List<Beam>();
            beams.Add(new Beam(i, 0, 'E'));
            while (beams.Count > 0)
            {
                p1.MoveBeam(input, tracker, beams);
            }
            int result = p1.Score(tracker);
            if (result > maxResult) maxResult = result;
        }
        for (int i = 0; i < h; i++)
        {
            List<char>[,] tracker = p1.CreateTracker(input);
            List<Beam> beams = new List<Beam>();
            beams.Add(new Beam(i, w - 1, 'W'));
            while (beams.Count > 0)
            {
                p1.MoveBeam(input, tracker, beams);
            }
            int result = p1.Score(tracker);
            if (result > maxResult) maxResult = result;
        }

        return maxResult;
    }
}