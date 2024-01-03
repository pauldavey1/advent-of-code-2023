using Day14;
using System.Diagnostics;

string[] input = File.ReadAllLines("input.txt");
char[,] charInput = new char[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        charInput[i, j] = input[i][j];
    }
}

{
    Puzzle1 p1 = new Puzzle1();
    Console.WriteLine(p1.answer(charInput));
    Stopwatch sw = new Stopwatch();
    sw.Start();
    Puzzle2 p2 = new Puzzle2();
    Console.WriteLine(p2.answer(charInput));
    Console.WriteLine(sw.Elapsed);
}