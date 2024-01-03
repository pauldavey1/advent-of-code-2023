namespace Day20;

public class Pulse
{
    public string Receiver { get; set; }
    public string Sender { get; set; }
    public bool isLow { get; set; }
    public Pulse(string receiver, string sender, bool b)
    {
        Receiver = receiver;
        Sender = sender;
        isLow = b;
    }
}

public static class PulseQueue
{
    private static readonly Queue<Pulse> GlobalQueue = new Queue<Pulse>();
    public static void Enqueue(Pulse p)
    {
        GlobalQueue.Enqueue(p);
    }
    public static Pulse Dequeue()
    {
        return GlobalQueue.Dequeue();
    }
    public static int Count()
    {
        return GlobalQueue.Count;
    }
}

public abstract class Module
{
    public string Name { get; set; }
    public char Type { get; set; }
    public List<string> Output { get; set; }
    public virtual Dictionary<string, bool>? Inputs { get; set; }
    public void SendPulse(bool isLow)
    {
        foreach (string s in Output)
        {
            PulseQueue.Enqueue(new Pulse(s, Name, isLow));
        }
    }
    public abstract void ReceivePulse(Pulse p);
    public virtual void PopulateInputs(List<Module> modules) { }
    public Module(string s)
    {
        string[] words = s.Split(' ');
        Name = words[0].Substring(1);
        Output = new List<string>();
        for (int i = 2; i < words.Length; i++)
        {
            Output.Add(words[i].Substring(0, 2));
        }
    }
}

public class Broadcast : Module
{
    public override void ReceivePulse(Pulse p)
    {
        if (!p.isLow) throw new ArgumentException();
        SendPulse(true);
    }
    public Broadcast(string s) : base(s)
    {
        Type = 'B';
        Name = "broadcaster";
    }
}

public class FlipFlop : Module
{
    public bool IsOn { get; set; }
    public override void ReceivePulse(Pulse p)
    {
        if (!p.isLow) return;
        if (IsOn)
        {
            SendPulse(true);
            IsOn = false;
        }
        else
        {
            SendPulse(false);
            IsOn = true;
        }
    }
    public FlipFlop(string s) : base(s)
    {
        Type = 'F';
        IsOn = false;
    }
}

public class Conjunction : Module
{
    public override Dictionary<string, bool> Inputs { get; set; }
    public override void ReceivePulse(Pulse p)
    {
        Inputs[p.Sender] = p.isLow;
        if (Inputs.ContainsValue(true)) { SendPulse(false); }
        else SendPulse(true);
    }
    public override void PopulateInputs(List<Module> modules)
    {
        foreach (Module m in modules)
        {
            if (m.Output.Contains(Name))
            {
                Inputs.Add(m.Name, true);
            }
        }
    }
    public Conjunction(string s) : base(s)
    {
        Type = 'C';
        Inputs = new Dictionary<string, bool>();
    }
}