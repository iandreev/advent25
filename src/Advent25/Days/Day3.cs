namespace Advent25.Days;

internal class Day3
{
    internal void Solve1()
    {
        var lines = GetInput();

        long result = 0;
        foreach (var line in lines)
        {
            var best = FindBest(line, 2);
            result += long.Parse(best);
        }

        Console.WriteLine(result);
    }

    string FindBest(string line, int digits)
    {
        if (digits == 0) return "";
        var max = line.ToCharArray().Take(line.Length - digits + 1).Max();
        var firstIndex = line.IndexOf(max);

        var sub = FindBest(line.Substring(firstIndex + 1), digits - 1);

        return max.ToString() + sub;
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        long result = 0;
        foreach (var line in lines)
        {
            var best = FindBest(line, 12);
            result += long.Parse(best);
        }

        Console.WriteLine(result);
    }


    List<string> GetInput() => File.ReadAllLines("Data/day3.txt")
        .ToList();

    List<string> GetInput2() => File.ReadAllLines("Data/day3_part2.txt")
        .ToList();
}
