using Day18;
using System.Diagnostics;

string[] input = File.ReadAllLines("input.txt");
List<Instruction> instructionsList = new List<Instruction>();
foreach (string line in input)
{
    string[] arr = line.Split(' ');
    instructionsList.Add(new Instruction(arr));
}

Puzzle1 p1 = new Puzzle1();
Console.WriteLine(p1.Answer(instructionsList));

Stopwatch sw = new Stopwatch();
sw.Start();
Puzzle2 p2 = new Puzzle2();
Console.WriteLine(p2.Answer(instructionsList));
Console.WriteLine(sw.Elapsed);
// 2m37s