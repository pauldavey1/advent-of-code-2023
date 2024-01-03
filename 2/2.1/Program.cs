string[] input = File.ReadAllLines("input.txt");

int result = 0;

foreach (string game in input)
{
    int colonIndex = game.IndexOf(':');
    int gameNumber = int.Parse(game.Substring(5, colonIndex - 5));

    string drawList = game.Substring(colonIndex + 2).Replace(';', ',');
    string[] draws = drawList.Split(", ");

    bool isValid = true;

    foreach (string draw in draws)
    {
        int spaceIndex = draw.IndexOf(' ');
        int count = int.Parse(draw.Substring(0, spaceIndex));
        string color = draw.Substring(spaceIndex + 1);
        if ((color == "red" && count > 12) || (color == "green" && count > 13) || (color == "blue" && count > 14))
        {
            isValid = false;
            break;
        }
    }

    if (isValid) result += gameNumber;
}

Console.WriteLine(result);