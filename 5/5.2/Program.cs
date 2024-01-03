// This is a brute-force solution that takes 15 minutes to run on my machine.
// Might come back and optimize if I have time.

using System.Linq;

string[] input = File.ReadAllLines("input.txt");

long[] seedPairs = input[0].Substring(7).Split(" ").Select(seed => long.Parse(seed)).ToArray();
List<long> seeds = new List<long>();
for (int i = 0; i < seedPairs.Length; i += 2)
{
    for (int j = 0; j < seedPairs[i + 1]; j++)
    {
        seeds.Add(seedPairs[i] + j);
    }
}


List<(long, long, long)> createMap(long first, long last)
{
    List<(long, long, long)> result = new List<(long, long, long)>();
    for (long i = first; i <= last; i++)
    {
        string[] values = input[i].Split(" ");
        result.Add((long.Parse(values[0]), long.Parse(values[1]), long.Parse(values[2])));
    }
    return result;
}

List<(long, long, long)> seedToSoil = createMap(3, 19);
List<(long, long, long)> soilToFertilizer = createMap(22, 30);
List<(long, long, long)> fertilizerToWater = createMap(33, 72);
List<(long, long, long)> waterToLight = createMap(75, 98);
List<(long, long, long)> lightToTemperature = createMap(101, 120);
List<(long, long, long)> temperatureToHumidity = createMap(123, 166);
List<(long, long, long)> humidityToLocation = createMap(169, 209);

List<(long, long, long)>[] maps = { seedToSoil, soilToFertilizer, fertilizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation };

long minimum = long.MaxValue;

foreach (long seed in seeds)
{
    long x = seed;

    foreach (List<(long, long, long)> map in maps)
    {
        foreach ((long, long, long) tuple in map)
        {
            if (x >= tuple.Item2 && x < tuple.Item2 + tuple.Item3)
            {
                x += (tuple.Item1 - tuple.Item2);
                break;
            }
        }
    }

    if (x < minimum) minimum = x;
}

Console.WriteLine(minimum);