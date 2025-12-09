namespace Advent25.Days;

internal class Day2
{
    internal void Solve1()
    {
        var lines = GetInput();

        HashSet<long> vis = new();
        for (long i = 1; i <= 99999; i++)
        {
            var c = i.ToString();
            vis.Add(long.Parse(c + c));
        }

        long result = 0;
        foreach (var item in lines)
        {
            for (long i = item.Start; i <= item.End; i++)
                result += vis.Contains(i) ? i : 0;
        }

        Console.WriteLine(result);
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        HashSet<long> vis = new();
        for (long i = 1; i <= 99999; i++)
        {
            var c = i.ToString();

            var len = 10 / c.Length;

            for (int j = 2; j <= len; j++)
                vis.Add(long.Parse(string.Join("", Enumerable.Range(1, j).Select(_=>c))));
        }

        long result = 0;
        foreach (var item in lines)
        {
            for (long i = item.Start; i <= item.End; i++)
                result += vis.Contains(i) ? i : 0;
        }

        Console.WriteLine(result);
    }


    List<Range> GetInput() => File.ReadAllText("Data/day2.txt")
        .Split(',')
        .Select(line =>
        {
            var s = line.Split('-');
            return new Range(long.Parse(s[0]), long.Parse(s[1]));
        })
        .ToList();

    List<Range> GetInput2() => File.ReadAllText("Data/day2_part2.txt")
        .Split(',')
        .Select(line =>
        {
            var s = line.Split('-');
            return new Range(long.Parse(s[0]), long.Parse(s[1]));
        })
        .ToList();

    record Range(long Start, long End);
}
