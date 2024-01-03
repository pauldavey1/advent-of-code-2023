namespace Day19;

public static class Puzzle1
{
    public static int Answer(string[] input)
    {
        Dictionary<string, List<Step>> workflows = new Dictionary<string, List<Step>>();
        List<Part> parts = new List<Part>();
        foreach (string line in input)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            int bracketIndex = line.IndexOf('{');
            if (bracketIndex == 0) parts.Add(new Part(line));
            else AddWorkflow(line, bracketIndex, workflows);
        }

        int result = 0;
        foreach (Part p in parts)
        {
            if (IsAccepted(p, workflows)) result += p.X + p.M + p.A + p.S;
        }
        return result;
    }

    public static void AddWorkflow(string line, int bracketIndex, Dictionary<string, List<Step>> workflows)
    {
        string name = line.Substring(0, bracketIndex);
        string[] steps = line.Substring(bracketIndex + 1, line.Length - bracketIndex - 2).Split(',');
        List<Step> stepList = new List<Step>();
        foreach (string stepString in steps)
        {
            stepList.Add(new Step(stepString));
        }
        workflows.Add(name, stepList);
    }

    public static bool IsAccepted(Part p, Dictionary<string, List<Step>> workflows)
    {
        while (p.Current != "A" && p.Current != "R")
        {
            TryWorkflow(p, workflows[p.Current]);
        }

        return p.Current == "A";
    }

    public static void TryWorkflow(Part p, List<Step> workflow)
    {
        for (int i = 0; i < workflow.Count; i++)
        {
            Step s = workflow[i];
            int valueToCompare = 0;
            switch (s.Category)
            {
                case '.':
                    p.Current = s.Outcome;
                    return;
                case 'x':
                    valueToCompare = p.X;
                    break;
                case 'm':
                    valueToCompare = p.M;
                    break;
                case 'a':
                    valueToCompare = p.A;
                    break;
                case 's':
                    valueToCompare = p.S;
                    break;
            }
            if ((s.GreaterThan && valueToCompare > s.Rating) || (!s.GreaterThan && valueToCompare < s.Rating))
            {
                p.Current = s.Outcome;
                return;
            }
        }
    }
}