namespace Day21;

public static class Puzzle1
{
    public static int Answer(string[] input, int steps)
    {
        (int r, int c) start = (-1, -1);
        var map = new char[input.Length, input[0].Length];
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                map[i, j] = input[i][j];
                if (map[i, j] == 'S')
                {
                    start = (i, j);
                }
            }
        }
        var coordinates = new List<(int r, int c)>();
        coordinates.Add(start);
        map[start.r, start.c] = 'E';

        for (int stepsCompleted = 0; stepsCompleted < steps; stepsCompleted++)
        {
            var newCoordinates = new List<(int r, int c)>();
            foreach ((int r, int c) loc in coordinates)
            {
                CheckSurroundings(loc, stepsCompleted, map, newCoordinates);
            }
            coordinates = newCoordinates;
        }

        // for (int i = 0; i < map.GetLength(0); i++)
        // {
        //     for (int j = 0; j < map.GetLength(1); j++)
        //     {
        //         Console.Write(map[i, j]);
        //     }
        //     Console.WriteLine();
        // }

        return ScoreMap(map, steps);
    }
    public static void CheckSurroundings((int r, int c) loc, int stepsCompleted, char[,] map, List<(int r, int c)> newCoordinates)
    {
        var checkList = new List<(int r, int c)> { (loc.r - 1, loc.c), (loc.r + 1, loc.c), (loc.r, loc.c - 1), (loc.r, loc.c + 1) };
        foreach ((int r, int c) newLoc in checkList)
        {
            if (newLoc.r < 0 || newLoc.c < 0 || newLoc.r > map.GetLength(0) - 1 || newLoc.c > map.GetLength(1) - 1) continue;
            if (map[newLoc.r, newLoc.c] == '.')
            {
                map[newLoc.r, newLoc.c] = stepsCompleted % 2 == 0 ? 'O' : 'E';
                newCoordinates.Add(newLoc);
            }
        }
    }

    public static int ScoreMap(char[,] map, int steps)
    {
        int count = 0;
        char matchChar = steps % 2 == 0 ? 'E' : 'O';
        foreach (char c in map)
        {
            if (c == matchChar) count++;
        }
        return count;
    }
}