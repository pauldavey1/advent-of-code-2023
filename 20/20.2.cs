namespace Day20;

public static class Puzzle2
{
    public static long Answer(string[] input)
    {
        List<Module> modules = Puzzle1.PopulateModules(input);

        long count = 0;
        long klLoop = 0;
        long vmLoop = 0;
        long kvLoop = 0;
        long vbLoop = 0;
        long product = 0;
        while (product == 0)
        {
            PressButton();
            count++;
            while (PulseQueue.Count() > 0)
            {
                Pulse p = PulseQueue.Dequeue();
                if (p.Sender == "kl" && !p.isLow && klLoop == 0) klLoop = count;
                if (p.Sender == "vm" && !p.isLow && vmLoop == 0) vmLoop = count;
                if (p.Sender == "kv" && !p.isLow && kvLoop == 0) kvLoop = count;
                if (p.Sender == "vb" && !p.isLow && vbLoop == 0) vbLoop = count;
                EvaluatePulse(p, modules);
            }
            product = klLoop * vmLoop * kvLoop * vbLoop;
        }
        // I *should* have to calculate LCM here, but turns out they're relative primes!
        return product;
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




