using Day13;

string[] input = File.ReadAllLines("input.txt");

{
    Puzzle1 p1 = new Puzzle1();
    Console.WriteLine(p1.answer(input));
    Puzzle2 p2 = new Puzzle2();
    Console.WriteLine(p2.answer(input));
}