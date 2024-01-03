string[] input = File.ReadAllLines("input.txt");

double result = 0;

foreach (string card in input)
{
    string[] winnerStrings = card.Substring(10, 29).Replace("  ", " ").Trim().Split(" ");
    string[] myStrings = card.Substring(42).Replace("  ", " ").Trim().Split(" ");

    double score = 0.5;

    foreach (string s in winnerStrings)
    {
        if (myStrings.Contains(s))
        {
            score *= 2;
        }
    }

    result += Math.Floor(score);
}

Console.WriteLine(result);