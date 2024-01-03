using Day25;

string[] input = File.ReadAllLines("input.txt");

// My code isn't guaranteed to get a correct answer
// This allows it to try repeatedly until it correctly identifies the three connections to remove
int answer = 0;
while (answer == 0)
{
    answer = Puzzle1.Answer(input);
}
Console.WriteLine(answer);