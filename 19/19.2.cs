namespace Day19;

public static class Puzzle2
{
    public static long Answer(string[] input)
    {
        Dictionary<string, List<Step>> workflows = new Dictionary<string, List<Step>>();
        foreach (string line in input)
        {
            if (string.IsNullOrWhiteSpace(line)) break;
            int bracketIndex = line.IndexOf('{');
            Puzzle1.AddWorkflow(line, bracketIndex, workflows);
        }

        List<PartBucket> partBuckets = new List<PartBucket>();
        partBuckets.Add(new PartBucket());

        long result = 0;
        long count = 0;
        while (partBuckets.Count > 0)
        {
            count++;
            PartBucket pb = partBuckets[0];
            if (pb.Current == "A")
            {
                result += TotalPartsInBucket(pb);
                partBuckets.Remove(pb);
            }
            else if (pb.Current == "R") partBuckets.Remove(pb);
            else TryPartBucketOnWorkflow(partBuckets, workflows[pb.Current]);
        }

        return result;
    }

    public static void TryPartBucketOnWorkflow(List<PartBucket> partBuckets, List<Step> workflow)
    {
        PartBucket pb = partBuckets[0];
        for (int i = 0; i < workflow.Count; i++)
        {
            Step s = workflow[i];
            int minValueToCompare = 0;
            int maxValueToCompare = 0;
            switch (s.Category)
            {
                case '.':
                    pb.Current = s.Outcome;
                    return;
                case 'x':
                    minValueToCompare = pb.MinX;
                    maxValueToCompare = pb.MaxX;
                    break;
                case 'm':
                    minValueToCompare = pb.MinM;
                    maxValueToCompare = pb.MaxM;
                    break;
                case 'a':
                    minValueToCompare = pb.MinA;
                    maxValueToCompare = pb.MaxA;
                    break;
                case 's':
                    minValueToCompare = pb.MinS;
                    maxValueToCompare = pb.MaxS;
                    break;
            }
            // If whole bucket passes test, change current and return
            if ((s.GreaterThan && minValueToCompare > s.Rating) || (!s.GreaterThan && maxValueToCompare < s.Rating))
            {
                pb.Current = s.Outcome;
                return;
            }
            // If none of bucket passes test, continue to next step
            if ((s.GreaterThan && maxValueToCompare <= s.Rating) || (!s.GreaterThan && minValueToCompare >= s.Rating))
            {
                continue;
            }
            // If part of bucket passes test, split bucket and add passing part to queue
            PartBucket passingBucket = new PartBucket(pb.MinX, pb.MaxX, pb.MinM, pb.MaxM, pb.MinA, pb.MaxA, pb.MinS, pb.MaxS, pb.Current);
            switch (s.Category)
            {
                case 'x':
                    if (s.GreaterThan)
                    {
                        passingBucket.MinX = s.Rating + 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MaxX = s.Rating;
                    }
                    else
                    {
                        passingBucket.MaxX = s.Rating - 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MinX = s.Rating;
                    }
                    break;
                case 'm':
                    if (s.GreaterThan)
                    {
                        passingBucket.MinM = s.Rating + 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MaxM = s.Rating;
                    }
                    else
                    {
                        passingBucket.MaxM = s.Rating - 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MinM = s.Rating;
                    }
                    break;
                case 'a':
                    if (s.GreaterThan)
                    {
                        passingBucket.MinA = s.Rating + 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MaxA = s.Rating;
                    }
                    else
                    {
                        passingBucket.MaxA = s.Rating - 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MinA = s.Rating;
                    }
                    break;
                case 's':
                    if (s.GreaterThan)
                    {
                        passingBucket.MinS = s.Rating + 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MaxS = s.Rating;
                    }
                    else
                    {
                        passingBucket.MaxS = s.Rating - 1;
                        passingBucket.Current = s.Outcome;
                        partBuckets.Add(passingBucket);
                        pb.MinS = s.Rating;
                    }
                    break;
            }
        }
    }

    public static long TotalPartsInBucket(PartBucket pb)
    {
        // Decomposing multiplication to get around overflowing Int32
        long result = 1;
        result *= pb.MaxX - pb.MinX + 1;
        result *= pb.MaxM - pb.MinM + 1;
        result *= pb.MaxA - pb.MinA + 1;
        result *= pb.MaxS - pb.MinS + 1;
        return result;
    }
}