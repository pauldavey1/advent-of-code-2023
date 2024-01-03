namespace Day25;

public class Component
{
    public string Name;
    public List<string> Connections;
    public Component(string s)
    {
        Name = s.Substring(0, 3);
        Connections = s.Substring(5).Split(' ').ToList();
    }
    public Component(string s, List<string> l)
    {
        Name = s;
        Connections = l;
    }
}