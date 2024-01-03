using Day17;
using System.Diagnostics;

string[] input = File.ReadAllLines("input.txt");
int[,] intInput = new int[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        intInput[i, j] = int.Parse(input[i][j].ToString());
    }
}

Puzzle1 p1 = new Puzzle1();
Stopwatch sw = new Stopwatch();
sw.Start();
Console.WriteLine(p1.Answer(intInput));
Console.WriteLine(sw.Elapsed);
// 6m47s

Puzzle2 p2 = new Puzzle2();
Stopwatch sw = new Stopwatch();
sw.Start();
Console.WriteLine(p2.Answer(intInput));
Console.WriteLine(sw.Elapsed);
// 1h33m07s