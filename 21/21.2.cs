namespace Day21;

public static class Puzzle2
{
    public static long Answer(string[] input, int steps)
    {
        // We'll only worry about odd steps since the final step count is odd
        long stepsDivisor = steps / 131;
        int stepsRemainder = steps % 131;
        // Divisor is 202300, remainder is 65

        // Create larger grid for testing repeats
        /*
        int gridCount = 19;
        int gridSize = 131;
        int stepsForGrid = gridSize * gridCount / 2;
        (int r, int c) start = (-1, -1);
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input.Length; j++)
            {
                if (input[i][j] == 'S')
                {
                    start = (i, j);
                }
            }
        }

        var map = new char[input.Length * gridCount, input[0].Length * gridCount];
        for (int i = 0; i < input.Length * gridCount; i++)
        {
            for (int j = 0; j < input[0].Length * gridCount; j++)
            {
                map[i, j] = input[i % gridSize][j % gridSize] == 'S' ? '.' : input[i % gridSize][j % gridSize];
            }
        }
        var coordinates = new List<(int r, int c)>();
        coordinates.Add((start.r + gridSize * (gridCount / 2), start.c + gridSize * (gridCount / 2)));
        map[start.r, start.c] = 'E';

        int oddCount = 0;

        for (int stepsCompleted = 0; stepsCompleted < stepsForGrid; stepsCompleted++)
        {
            if (stepsCompleted % (gridSize) == stepsRemainder)
            {
                Console.WriteLine($"{stepsCompleted / gridSize}, odd: {oddCount}");
            }
            var newCoordinates = new List<(int r, int c)>();
            foreach ((int r, int c) loc in coordinates)
            {
                CheckSurroundings(loc, stepsCompleted, map, newCoordinates, ref oddCount);
            }
            coordinates = newCoordinates;
        }
        */

        // A lot of this was easier to do by hand
        // Drawing on paper shows the approximate pattern as it expands by 131 each time
        // Can classify shapes:
        // home = missing 2 corners (set of 4)
        // corner = just 1 corner (set of 4)
        // chamfer = missing 1 corner (set of 4)
        // full = full square
        // After 65 + 131*x steps, we have the following "shapes":
        // x=1: 1 home, 1 corner, 0 chamfer, 1 full
        // x=2: 1 home, 2 corner, 1 chamfer, 5 full
        // x=3: 1 home, 3 corner, 2 chamfer, 13 full
        // x=4: 1 home, 4 corner, 3 chamfer, 25 full
        // x=n: 1 home, 4n corner, 4n-1 chamfer, n*n + (n-1)*(n-1) full

        // We can use a partial grid to find the following accessible steps, given x:
        // x=1: 32657
        // x=2: 91439
        // x=3: 178341
        // x=4: 295907
        // x=5: 440745
        // x=6: 617095
        // x=7: 819869
        // x=8: 1055003

        // Plug x=2, x=4, x=6 into a quadratic equation solver
        // Can't totally differentiate between home, corner, chamfer because relationship is linear – but don't need to
        // corner = 25680-home, chamfer = home + 3604, full = 7295
        // Good news: other tests also gave me full = 7295, so this seems plausible
        // Now solve for x = 202300
        return stepsDivisor * 25680 + (stepsDivisor - 1) * 3604 + (stepsDivisor * stepsDivisor + (stepsDivisor - 1) * (stepsDivisor - 1)) * 7295;
    }
    public static void CheckSurroundings((int r, int c) loc, int stepsCompleted, char[,] map, List<(int r, int c)> newCoordinates, ref int oddCount)
    {
        var checkList = new List<(int r, int c)> { (loc.r - 1, loc.c), (loc.r + 1, loc.c), (loc.r, loc.c - 1), (loc.r, loc.c + 1) };
        foreach ((int r, int c) newLoc in checkList)
        {
            if (newLoc.r < 0 || newLoc.c < 0 || newLoc.r > map.GetLength(0) - 1 || newLoc.c > map.GetLength(1) - 1) continue;
            if (map[newLoc.r, newLoc.c] == '.')
            {
                map[newLoc.r, newLoc.c] = stepsCompleted % 2 == 0 ? 'O' : 'E';
                if (stepsCompleted % 2 == 0) oddCount++;
                newCoordinates.Add(newLoc);
            }
        }
    }
}