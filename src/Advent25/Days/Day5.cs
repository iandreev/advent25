namespace Advent25.Days;

internal class Day5
{
    internal void Solve1()
    {
        var lines = GetInput();

        long result = 0;

        foreach (var item in lines.Numbers)
        {
            result += lines.Ranges.Any(r => r.Start <= item && r.End >= item) ? 1 : 0;
        }

        Console.WriteLine(result);
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        long result = 0;

        var ranges = lines.Ranges.OrderBy(x => x.Start).ThenBy(x => x.End).ToList();
        List<Range> mergedRanges = [ranges[0]];

        for (int i = 1; i < ranges.Count; i++)
        {
            if (ranges[i].Start > mergedRanges.Last().End)
            {
                mergedRanges.Add(ranges[i]);
            }
            else
            {
                var last = mergedRanges.Last();
                mergedRanges.RemoveAt(mergedRanges.Count - 1);
                mergedRanges.Add(new Range(last.Start, Math.Max(last.End, ranges[i].End)));
            }
        }

        foreach (var item in mergedRanges)
        {
            result += item.End - item.Start + 1;
        }

        Console.WriteLine(result);
    }


    Data GetInput()
    {
        var lines = File.ReadAllLines("Data/day5.txt");
        var data = new Data(new(), new());
        foreach (var line in lines)
        {
            if (line.Contains('-'))
            {
                var a = line.Split('-').Select(long.Parse).ToList();
                data.Ranges.Add(new Range(a[0], a[1]));
            }
            else if (line != "")
                data.Numbers.Add(long.Parse(line));
        }
        return data;
    }

    Data GetInput2()
    {
        var lines = File.ReadAllLines("Data/day5_part2.txt");
        var data = new Data(new(), new());
        foreach (var line in lines)
        {
            if (line.Contains('-'))
            {
                var a = line.Split('-').Select(long.Parse).ToList();
                data.Ranges.Add(new Range(a[0], a[1]));
            }
            else if (line != "")
                data.Numbers.Add(long.Parse(line));
        }
        return data;
    }

    record Range(long Start, long End);
    record Data(List<Range> Ranges, List<long> Numbers);
}
