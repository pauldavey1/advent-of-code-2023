namespace Day23;

public static class Puzzle2
{
    public static int Answer(char[,] map)
    {
        var trails = Puzzle1.GetTrails(map);

        return FindLongestPath(trails, (0, 1), new List<(int, int)>());
    }

    public static int FindLongestPath(List<Trail> trails, (int, int) currentNode, List<(int, int)> visitedNodes)
    {
        var newPaths = trails.Where(trail => (trail.Start == currentNode && !visitedNodes.Contains(trail.End)) || (trail.End == currentNode && !visitedNodes.Contains(trail.Start))).ToList();
        var newVisitedNodes = new List<(int, int)>(visitedNodes);
        newVisitedNodes.Add(currentNode);
        if (newPaths.Count == 1) return newPaths[0].Length + 1 + FindLongestPath(trails, newPaths[0].Start == currentNode ? newPaths[0].End : newPaths[0].Start, newVisitedNodes);
        if (newPaths.Count == 2) return Math.Max(newPaths[0].Length + 1 + FindLongestPath(trails, newPaths[0].Start == currentNode ? newPaths[0].End : newPaths[0].Start, newVisitedNodes), newPaths[1].Length + 1 + FindLongestPath(trails, newPaths[1].Start == currentNode ? newPaths[1].End : newPaths[1].Start, newVisitedNodes));
        if (newPaths.Count == 3) return Math.Max(Math.Max(newPaths[0].Length + 1 + FindLongestPath(trails, newPaths[0].Start == currentNode ? newPaths[0].End : newPaths[0].Start, newVisitedNodes), newPaths[1].Length + 1 + FindLongestPath(trails, newPaths[1].Start == currentNode ? newPaths[1].End : newPaths[1].Start, newVisitedNodes)), newPaths[2].Length + 1 + FindLongestPath(trails, newPaths[2].Start == currentNode ? newPaths[2].End : newPaths[2].Start, newVisitedNodes));
        return currentNode.Item1 < 140 ? -10000 : 0;
    }
}