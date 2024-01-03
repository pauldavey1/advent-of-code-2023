string[] input = File.ReadAllLines("input.txt");

int result = 0;

foreach (string game in input)
{
    string[] draws = game.Substring(game.IndexOf(':') + 2).Replace(';', ',').Split(", ");

    int blue = 0;
    int green = 0;
    int red = 0;

    foreach (string draw in draws)
    {
        int spaceIndex = draw.IndexOf(' ');
        int count = int.Parse(draw.Substring(0, spaceIndex));
        string color = draw.Substring(spaceIndex + 1);
        if (color == "blue" && count > blue)
        {
            blue = count;
        }
        if (color == "green" && count > green)
        {
            green = count;
        }
        if (color == "red" && count > red)
        {
            red = count;
        }

    }

    result += blue * green * red;
}

Console.WriteLine(result);