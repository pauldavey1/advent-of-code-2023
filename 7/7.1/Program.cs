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

// Prepare cards so that alphabetical order is card value order
string normalizeCards(string cards)
{
    return cards.Replace('A', 'a').Replace('K', 'b').Replace('Q', 'c').Replace('J', 'd').Replace('T', 'e').Replace('9', 'f').Replace('8', 'g').Replace('7', 'h').Replace('6', 'i').Replace('5', 'j').Replace('4', 'k').Replace('3', 'l').Replace('2', 'm');
}

string calculateSortKey(string cards)
{
    // Count cards in hand
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

    var sortedCardCount = cardCount.OrderByDescending(pair => pair.Value);

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


// Didn't read the instructions and tried sorting like actual poker hands:
// string calculateSortKey(string cards)
// {
//     // Count cards in hand
//     Dictionary<char, int> cardCount = new Dictionary<char, int>();
//     foreach (char c in cards)
//     {
//         if (cardCount.ContainsKey(c))
//         {
//             cardCount[c]++;
//         }
//         else
//         {
//             cardCount[c] = 1;
//         }
//     }

//     var sortedCardCount = cardCount.OrderByDescending(pair => pair.Value).ThenBy(pair => pair.Key);

//     if (sortedCardCount.ElementAt(0).Value == 5)
//     {
//         return "A" + sortedCardCount.ElementAt(0).Key;
//     }
//     if (sortedCardCount.ElementAt(0).Value == 4)
//     {
//         return "B" + sortedCardCount.ElementAt(0).Key + sortedCardCount.ElementAt(1).Key;
//     }
//     if (sortedCardCount.ElementAt(0).Value == 3)
//     {
//         if (sortedCardCount.ElementAt(1).Value == 2)
//         {
//             return "C" + sortedCardCount.ElementAt(0).Key + sortedCardCount.ElementAt(1).Key;
//         }
//         else
//         {
//             return "D" + sortedCardCount.ElementAt(0).Key + sortedCardCount.ElementAt(1).Key + sortedCardCount.ElementAt(2).Key;
//         }
//     }
//     if (sortedCardCount.ElementAt(0).Value == 2)
//     {
//         if (sortedCardCount.ElementAt(1).Value == 2)
//         {
//             return "E" + sortedCardCount.ElementAt(0).Key + sortedCardCount.ElementAt(1).Key + sortedCardCount.ElementAt(2).Key;
//         }
//         else
//         {
//             return "F" + sortedCardCount.ElementAt(0).Key + sortedCardCount.ElementAt(1).Key + sortedCardCount.ElementAt(2).Key + sortedCardCount.ElementAt(3).Key;
//         }
//     }
//     else return "G" + sortedCardCount.ElementAt(0).Key + sortedCardCount.ElementAt(1).Key + sortedCardCount.ElementAt(2).Key + sortedCardCount.ElementAt(3).Key + sortedCardCount.ElementAt(4).Key;
// }