namespace Day25;

public static class Puzzle1
{
    public static int Answer(string[] input)
    {
        List<Component> components = new List<Component>();
        foreach (string s in input)
        {
            components.Add(new Component(s));
        }

        // Duplicate each connection so connection list for each component is complete
        var reverseConnections = new List<(string, string)>();
        foreach (Component c in components)
        {
            foreach (string name in c.Connections)
            {
                reverseConnections.Add((name, c.Name));
            }
        }
        foreach ((string name, string connectedName) in reverseConnections)
        {
            if (!components.Exists(c => c.Name == name)) components.Add(new Component(name, new List<string>()));
            components.Find(c => c.Name == name)!.Connections.Add(connectedName);
        }


        // Pick 100 random pairs and find shortest path, noting how often each connection appears
        var pathCount = new Dictionary<(string, string), int>();
        foreach (Component c in components)
        {
            foreach (string s in c.Connections)
            {
                if (string.Compare(c.Name, s, StringComparison.CurrentCultureIgnoreCase) < 0) pathCount.Add((c.Name, s), 0);
            }
        }
        for (int i = 1; i <= 100; i++)
        {
            List<(string, string)> connectionPath = FindRandomPath(components);
            foreach ((string, string) connection in connectionPath)
            {
                pathCount[connection]++;
            }
        }

        // Identify the most likely connections between the independent groups
        var sortedPathCount = pathCount.ToList();
        sortedPathCount.Sort((p1, p2) => p2.Value - p1.Value);
        var connectionsToRemove = new List<(string, string)>();
        var usedNodes = new List<string>();
        for (int i = 0; i < sortedPathCount.Count; i++)
        {
            // Add the most frequently observed connections, but don't add those directly connected to an already-added connection
            // This isn't guaranteed to provide the top 3, but there's a reasonable chance
            if (!usedNodes.Contains(sortedPathCount[i].Key.Item1) && !usedNodes.Contains(sortedPathCount[i].Key.Item2))
            {
                Console.WriteLine($"{sortedPathCount[i].Key.Item1}-{sortedPathCount[i].Key.Item2}, {sortedPathCount[i].Value}");
                connectionsToRemove.Add(sortedPathCount[i].Key);
                usedNodes.Add(sortedPathCount[i].Key.Item1);
                usedNodes.Add(sortedPathCount[i].Key.Item2);
            }
            if (connectionsToRemove.Count == 3) break;
        }

        // Remove the three probable connections
        foreach ((string, string) connection in connectionsToRemove)
        {
            components.Find(c => c.Name == connection.Item1)!.Connections.Remove(connection.Item2);
            components.Find(c => c.Name == connection.Item2)!.Connections.Remove(connection.Item1);
        }

        // Pick a component to find the size of its group; then multiply by the number of components not connected to that group
        int groupSize = FindGroupSize(components);
        return groupSize * (components.Count - groupSize);
    }

    public static List<(string, string)> FindRandomPath(List<Component> cl)
    {
        // Pick random start & end points
        Random r = new Random();
        Component start = cl[r.Next(0, cl.Count)];
        Component end = cl[r.Next(0, cl.Count)];
        // Create queue of paths to check with current component and list of already-visited nodes
        var q = new Queue<(Component, List<string>)>();
        q.Enqueue((start, new List<string>()));
        var connectionPath = new List<(string, string)>();
        while (connectionPath.Count == 0)
        {
            (Component c, List<string> path) curr = q.Dequeue();
            foreach (string next in curr.c.Connections)
            {
                if (curr.c.Name == end.Name)
                {
                    curr.path.Add(curr.c.Name);
                    for (int i = 0; i < curr.path.Count - 1; i++)
                    {
                        connectionPath.Add(string.Compare(curr.path[i], curr.path[i + 1], StringComparison.CurrentCultureIgnoreCase) < 0 ? (curr.path[i], curr.path[i + 1]) : (curr.path[i + 1], curr.path[i]));
                    }
                    break;
                }
                if (!curr.path.Contains(next))
                {
                    var newPath = new List<string>(curr.path);
                    newPath.Add(curr.c.Name);
                    q.Enqueue((cl.Find(c => c.Name == next)!, newPath));
                }
            }
        }

        return connectionPath;
    }

    public static int FindGroupSize(List<Component> cl)
    {
        var group = new List<string>();
        group.Add(cl[0].Name);
        var q = new Queue<Component>();
        q.Enqueue(cl[0]);
        while (q.Count > 0)
        {
            Component curr = q.Dequeue();
            foreach (string next in curr.Connections)
            {
                if (!group.Contains(next))
                {
                    group.Add(next);
                    q.Enqueue((cl.Find(c => c.Name == next)!));
                }
            }
        }
        return group.Count;
    }
}