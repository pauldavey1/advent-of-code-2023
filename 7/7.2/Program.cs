string[] input = File.ReadAllLines("input.txt");

Hand[] hands = input.Select(line => new Hand(line)).ToArray();

foreach (Hand hand in hands)
{
    hand.cards = normalizeCards(hand.cards);
    hand.sortKey = calculateSortKey(hand.cards);
}

var sortedHands = hands.OrderByDescending(hand => hand.sortKey).ThenByDescending(hand => hand.cards);

int rank = 1;
int result = 0;

foreach (Hand hand in sortedHands)
{
    result += hand.bid * rank;
    rank++;
}

Console.WriteLine(result);

string normalizeCards(string cards)
{
    return cards.Replace('A', 'a').Replace('K', 'b').Replace('Q', 'c').Replace('T', 'd').Replace('9', 'e').Replace('8', 'f').Replace('7', 'g').Replace('6', 'h').Replace('5', 'i').Replace('4', 'j').Replace('3', 'k').Replace('2', 'l').Replace('J', 'm');
}

string calculateSortKey(string cards)
{
    Dictionary<char, int> cardCount = new Dictionary<char, int>();
    foreach (char c in cards)
    {
        if (cardCount.ContainsKey(c))
        {
            cardCount[c]++;
        }
        else
        {
            cardCount[c] = 1;
        }
    }

    Dictionary<char, int> sortedCardCount = cardCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

    if (sortedCardCount.ContainsKey('m'))
    {
        char first = sortedCardCount.ElementAt(0).Key;
        if (first == 'm')
        {
            if (sortedCardCount.Count == 1)
            {
                first = 'a';
                sortedCardCount[first] = 0;
            }
            else
            {
                first = sortedCardCount.ElementAt(1).Key;
            }
        }
        sortedCardCount[first] += sortedCardCount['m'];
        sortedCardCount.Remove('m');
    }

    if (sortedCardCount.ElementAt(0).Value == 5)
    {
        return "A";
    }
    if (sortedCardCount.ElementAt(0).Value == 4)
    {
        return "B";
    }
    if (sortedCardCount.ElementAt(0).Value == 3)
    {
        if (sortedCardCount.ElementAt(1).Value == 2)
        {
            return "C";
        }
        else
        {
            return "D";
        }
    }
    if (sortedCardCount.ElementAt(0).Value == 2)
    {
        if (sortedCardCount.ElementAt(1).Value == 2)
        {
            return "E";
        }
        else
        {
            return "F";
        }
    }
    else return "G";
}

class Hand
{
    public string cards;
    public int bid;
    public string sortKey;

    public Hand(string line)
    {
        cards = line.Substring(0, 5);
        bid = int.Parse(line.Substring(6));
    }
}