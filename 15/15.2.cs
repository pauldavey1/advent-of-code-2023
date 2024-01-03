namespace Day15;

public class Lens
{
    public string Label { get; set; }
    public int FocalLength { get; set; }
    public Lens(string s)
    {
        if (s.Contains('='))
        {
            Label = s.Substring(0, s.Length - 2);
            FocalLength = int.Parse(s.Substring(s.Length - 1));
        }
        else
        {
            Label = Label = s.Substring(0, s.Length - 1);
            FocalLength = 0;
        }
    }
}

public class Puzzle2
{
    Puzzle1 p1 = new Puzzle1();

    public int Answer(string[] input)
    {
        List<Lens>[] boxes = new List<Lens>[256];
        for (int i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new List<Lens>();
        }
        foreach (string s in input)
        {
            Lens l = new Lens(s);
            if (l.FocalLength > 0) Add(l, boxes);
            else Remove(l, boxes);
        }
        return Total(boxes);
    }

    public void Add(Lens l, List<Lens>[] boxes)
    {
        int boxNumber = p1.hash(l.Label);
        List<Lens> box = boxes[boxNumber];
        bool hasLens = box.Any(obj => obj.Label == l.Label);
        if (hasLens)
        {
            Lens existingLens = box.FirstOrDefault(obj => obj.Label == l.Label);
            existingLens.FocalLength = l.FocalLength;
        }
        else box.Add(l);
    }

    public void Remove(Lens l, List<Lens>[] boxes)
    {
        int boxNumber = p1.hash(l.Label);
        List<Lens> box = boxes[boxNumber];
        box.Remove(box.FirstOrDefault(obj => obj.Label == l.Label));
    }

    public int Total(List<Lens>[] boxes)
    {
        int result = 0;
        for (int i = 0; i < boxes.Length; i++)
        {
            List<Lens> box = boxes[i];
            for (int j = 0; j < box.Count; j++)
            {
                result += (i + 1) * (j + 1) * box[j].FocalLength;
            }
        }
        return result;
    }
}