using Day20;

public static class Puzzle1
{
    public static long Answer(string[] input)
    {
        List<Module> modules = PopulateModules(input);

        long lowCount = 0;
        long highCount = 0;
        for (int i = 0; i < 1000; i++)
        {
            PressButton();
            while (PulseQueue.Count() > 0)
            {
                Pulse p = PulseQueue.Dequeue();
                if (p.isLow) lowCount++;
                else highCount++;
                EvaluatePulse(p, modules);
            }
        }
        return highCount * lowCount;
    }

    public static List<Module> PopulateModules(string[] input)
    {
        List<Module> modules = new List<Module>();
        foreach (string s in input)
        {
            if (s[0] == 'b') modules.Add(new Broadcast(s));
            if (s[0] == '%') modules.Add(new FlipFlop(s));
            if (s[0] == '&') modules.Add(new Conjunction(s));
        }
        foreach (Module m in modules)
        {
            m.PopulateInputs(modules);
        }
        return modules;
    }
    public static void PressButton()
    {
        PulseQueue.Enqueue(new Pulse("broadcaster", "button", true));
    }

    public static void EvaluatePulse(Pulse p, List<Module> modules)
    {
        Module m = modules.Find(m => m.Name == p.Receiver);
        if (m != null) m.ReceivePulse(p);
    }
}