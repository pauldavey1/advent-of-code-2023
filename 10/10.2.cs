namespace Day10;

class Puzzle2
{
    public int answer(string[] input)
    {

        SharedUtils utils = new SharedUtils();

        // In part 1, wrote out code to determine loop start programmatically as an exercise
        // But it's obvious from looking at the map that there are only two valid departures from the start, and they create one loop
        // So it's easier to hard-code the values and follow that loop in one direction
        (int r, int c) start = (20, 103);
        // Start at the pipe below the 'S'
        (char dir, int r, int c) current = ('S', 21, 103);

        // Create a new array to track loop characters
        int[,] loopTracker = new int[input.Length, input[0].Length];
        loopTracker[start.r, start.c] = 1;
        while (input[current.r][current.c] != 'S')
        {
            loopTracker[current.r, current.c] = 1;
            current = utils.FindNext(input, current);
        }

        // Turn all non-loop characters in input to periods
        string[] cleanedInput = new string[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            string newLine = "";
            for (int j = 0; j < input[i].Length; j++)
            {
                if (loopTracker[i, j] == 0)
                {
                    newLine += ".";
                }
                // Correct start
                else if (i == start.r && j == start.c)
                {
                    newLine += "7";
                }
                else
                {
                    newLine += input[i][j];
                }
            }
            cleanedInput[i] = newLine;
        }

        // Print map
        // for (int i = 0; i < 140; i++)
        // {
        //     for (int j = 0; j < 140; j++)
        //     {

        //         Console.Write(cleanedInput[i][j]);
        //     }
        //     Console.WriteLine();
        // }

        // Count interior spaces by checking when boundary is crossed
        int result = 0;
        foreach (string line in cleanedInput)
        {
            bool inside = false;
            char from = 'X';
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '.')
                {
                    if (inside)
                    {
                        result++;
                    }
                }
                if (c == '|') inside = !inside;
                if (c == 'F') from = 'S';
                if (c == 'L') from = 'N';
                if (c == '7')
                {
                    if (from == 'N') inside = !inside;
                    from = 'X';
                }
                if (c == 'J')
                {
                    if (from == 'S') inside = !inside;
                    from = 'X';
                }
            }
        }
        return result;
    }
}
