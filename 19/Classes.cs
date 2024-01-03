namespace Day19;

public class Step
{
    public char Category;
    public bool GreaterThan;
    public int Rating;
    public string Outcome;
    public Step(string s)
    {
        int colonIndex = s.IndexOf(':');
        if (colonIndex == -1)
        {
            // If no colon, we're ready to skip to outcome without evaluating
            Category = '.';
            Outcome = s;
        }
        else
        {
            Category = s[0];
            GreaterThan = s[1] == '>';
            Rating = int.Parse(s.Substring(2, colonIndex - 2));
            Outcome = s.Substring(colonIndex + 1);
        }
    }

}

public class Part
{
    public int X;
    public int M;
    public int A;
    public int S;
    public string Current;
    public Part(string s)
    {
        string[] ratings = s.Split(',');
        X = int.Parse(ratings[0].Substring(3));
        M = int.Parse(ratings[1].Substring(2));
        A = int.Parse(ratings[2].Substring(2));
        S = int.Parse(ratings[3].Substring(2, ratings[3].Length - 3));
        Current = "in";
    }
}

public class PartBucket
{
    public int MinX;
    public int MaxX;
    public int MinM;
    public int MaxM;
    public int MinA;
    public int MaxA;
    public int MinS;
    public int MaxS;
    public string Current;
    public PartBucket(int minx = 1, int maxx = 4000, int minm = 1, int maxm = 4000, int mina = 1, int maxa = 4000, int mins = 1, int maxs = 4000, string current = "in")
    {
        MinX = minx;
        MaxX = maxx;
        MinM = minm;
        MaxM = maxm;
        MinA = mina;
        MaxA = maxa;
        MinS = mins;
        MaxS = maxs;
        Current = current;
    }
}