namespace Day23;

public static class Puzzle1
{
    public static int Answer(char[,] map)
    {
        // Create list of all trail segments
        // Note: input modified to add v after start position and before end position
        var trails = GetTrails(map);

        // Find longest possible path from start to end without revisits
        return FindLongestPath(trails, (0, 1), new List<(int, int)>());
    }

    public static List<Trail> GetTrails(char[,] map)
    {
        var trails = new List<Trail>();
        // Find all start segments and map trails from there
        for (int r = 0; r < map.GetLength(0); r++)
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                // If it doesn't border a contradictory arrow, it's the start of a trail
                if ((map[r, c] == '>' && !GetNESW((r, c + 1), map).Contains('v')) || (map[r, c] == 'v' && !GetNESW((r + 1, c), map).Contains('>')))
                {
                    CreateTrail((r, c), map, trails);
                }
            }
        }
        return trails;
    }

    public static string GetNESW((int r, int c) pos, char[,] map)
    {
        string result = "";
        result += pos.r != 0 ? map[pos.r - 1, pos.c] : '#';
        result += pos.c != map.GetLength(1) - 1 ? map[pos.r, pos.c + 1] : '#';
        result += pos.r != map.GetLength(0) - 1 ? map[pos.r + 1, pos.c] : '#';
        result += pos.c != 0 ? map[pos.r, pos.c - 1] : '#';
        return result;
    }

    public static void CreateTrail((int r, int c) pos, char[,] map, List<Trail> trails)
    {
        int r = pos.r;
        int c = pos.c;
        int length = 2;
        char dir = map[r, c] == 'v' ? 'S' : 'E';
        if (dir == 'S') r++; else c++;
        while (map[r, c] == '.')
        {
            if (r == map.GetLength(0)) return;
            string nesw = GetNESW((r, c), map);
            if (dir != 'S' && nesw[0] != '#')
            {
                length++;
                r--;
                dir = 'N';
            }
            else if (dir != 'W' && nesw[1] != '#')
            {
                length++;
                c++;
                dir = 'E';
            }
            else if (dir != 'N' && nesw[2] != '#')
            {
                length++;
                r++;
                dir = 'S';
            }
            else if (dir != 'E' && nesw[3] != '#')
            {
                length++;
                c--;
                dir = 'W';
            }
            // If there's not a valid next step, we've reached a dead end and shouldn't add this trail
            else return;
        }
        // End node is period past the final slope
        if (map[r, c] == 'v') r++; else c++;
        // Start node is period before the first slope
        if (GetNESW(pos, map)[0] == '.') pos.r--; else pos.c--;
        trails.Add(new Trail(length, pos, (r, c)));
        return;
    }

    public static int FindLongestPath(List<Trail> trails, (int, int) currentNode, List<(int, int)> visitedNodes)
    {
        // Edge cases not handled: trail that loops back on itself immediately; path that ends in visited node but is longer than longest path to end
        var newPaths = trails.Where(trail => trail.Start == currentNode && !visitedNodes.Contains(trail.End)).ToList();
        var newVisitedNodes = new List<(int, int)>(visitedNodes);
        newVisitedNodes.Add(currentNode);
        if (newPaths.Count == 1) return newPaths[0].Length + 1 + FindLongestPath(trails, newPaths[0].End, newVisitedNodes);
        if (newPaths.Count == 2) return Math.Max(newPaths[0].Length + 1 + FindLongestPath(trails, newPaths[0].End, newVisitedNodes), newPaths[1].Length + 1 + FindLongestPath(trails, newPaths[1].End, newVisitedNodes));
        return 0;
    }
}