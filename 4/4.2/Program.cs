string[] input = File.ReadAllLines("input.txt");
int[] cards = new int[input.Length];
Array.Fill(cards, 1);

for (int i = 0; i < input.Length; i++)
{
    string card = input[i];
    string[] winnerStrings = card.Substring(10, 29).Replace("  ", " ").Trim().Split(" ");
    string[] myStrings = card.Substring(42).Replace("  ", " ").Trim().Split(" ");
    int count = 0;

    foreach (string s in winnerStrings)
    {
        if (myStrings.Contains(s))
        {
            count++;
        }
    }

    for (int j = i + 1; j < i + count + 1; j++)
    {
        cards[j] += cards[i];
    }
}

int result = 0;

foreach (int count in cards)
{
    result += count;
}

Console.WriteLine(result);