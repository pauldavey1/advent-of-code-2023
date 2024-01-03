using Day23;

string[] input = File.ReadAllLines("input.txt");
char[,] charInput = new char[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        charInput[i, j] = input[i][j];
    }
}

Console.WriteLine(Puzzle1.Answer(charInput));
Console.WriteLine(Puzzle2.Answer(charInput));