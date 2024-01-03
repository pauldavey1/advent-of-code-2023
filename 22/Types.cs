public class Coordinate
{
    public int X;
    public int Y;
    public int Z;
    public Coordinate(string s)
    {
        string[] xyz = s.Split(',');
        X = int.Parse(xyz[0]);
        Y = int.Parse(xyz[1]);
        Z = int.Parse(xyz[2]);
    }
}

public class Brick
{
    public Coordinate Start;
    public Coordinate End;
    public int LowestZ;
    public int Length;
    public Brick(string s)
    {
        string[] coords = s.Split('~');
        Start = new Coordinate(coords[0]);
        End = new Coordinate(coords[1]);
        LowestZ = Math.Min(Start.Z, End.Z);
        // Only varies in one dimension so can just add differences (since 2 will be 0)
        Length = Math.Abs(End.X - Start.X) + Math.Abs(End.Y - Start.Y) + Math.Abs(End.Z - Start.Z) + 1;
    }
}

public class BrickPosition
{
    public int Number;
    public List<int> BricksBelow;
    public List<int> BricksAbove;
    public BrickPosition(int i)
    {
        Number = i;
        BricksBelow = new List<int>();
        BricksAbove = new List<int>();
    }

}