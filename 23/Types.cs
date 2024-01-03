public class Trail
{
    public int Length;
    public (int, int) Start;
    public (int, int) End;
    public Trail(int l, (int, int) s, (int, int) e)
    {
        Length = l;
        Start = s;
        End = e;
    }
}