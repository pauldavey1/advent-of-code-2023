public class Hailstone
{
    public long Px;
    public long Py;
    public long Pz;
    public long Vx;
    public long Vy;
    public long Vz;
    public double XYSlope;
    public double YIntercept;
    public Hailstone(string s)
    {
        string[] arr = s.Split(", ", StringSplitOptions.RemoveEmptyEntries);
        Px = long.Parse(arr[0]);
        Py = long.Parse(arr[1]);
        Vy = long.Parse(arr[3]);
        Vz = long.Parse(arr[4]);
        string[] arr2 = arr[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        Pz = long.Parse(arr2[0]);
        Vx = long.Parse(arr2[2]);
        XYSlope = (double)Vy / Vx;
        YIntercept = Py - XYSlope * Px;
    }
    public Hailstone(double px, double py, double pz, double vx, double vy, double vz)
    {
        Px = (long)px;
        Py = (long)py;
        Pz = (long)pz;
        Vx = (long)vx;
        Vy = (long)vy;
        Vz = (long)vz;
        XYSlope = (double)Vy / Vx;
        YIntercept = Py - XYSlope * Px;
    }
}