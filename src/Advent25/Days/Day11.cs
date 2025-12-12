namespace Advent25.Days;

internal class Day11
{
    internal void Solve1()
    {
        var lines = GetInput();

        double result = Foo("you", lines);

        Console.WriteLine(result);
    }

    int Foo(string name, Dictionary<string, List<string>> map)
    {
        if (name == "out") return 1;

        var result = 0;
        foreach (var link in map[name])
        {
            result += Foo(link, map);
        }
        return result;
    }

    internal void Solve2()
    {
        var lines = GetInput2();
        _map = new();
        double s_f = Foo15("svr", "fft", ["dac"], lines);
        _map = new();
        double s_d = Foo15("svr", "dac", ["fft"], lines);

        _map = new();
        double f_d = Foo15("fft", "dac", [""], lines);
        _map = new();
        double d_f = Foo15("dac", "fft", [""], lines);

        _map = new(); 
        double f_o = Foo15("fft", "out", [""], lines);
        _map = new(); 
        double d_o = Foo15("dac", "out", [""], lines);

        double result = s_f * f_d * d_o + s_d * d_f * f_o;
        Console.WriteLine(result);
    }

    Dictionary<(string, string), long> _map = new();
    long Foo15(string name, string target, HashSet<string> exclude, Dictionary<string, List<string>> map)
    {
        if (_map.ContainsKey((name, target)))
            return _map[(name, target)];

        long result = 0;
        if (name == target) result = 1;
        else
        if (name == "out") result = 0;
        else if (exclude.Contains(name)) result = 0;
        else
        {
            foreach (var link in map[name])
            {
                result += Foo15(link, target, exclude, map);
            }
        }

        _map[(name, target)] = result;
        return result;
    }

    Dictionary<string, List<string>> GetInput() => File.ReadAllLines("Data/day11.txt")
        .Select(line =>
        {
            var ps = line.Split(':');
            var name = ps[0];
            var outs = ps[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            return new { Name = name, Outs = outs };
        })
        .ToDictionary(x => x.Name, v => v.Outs);

    Dictionary<string, List<string>> GetInput2() => File.ReadAllLines("Data/day11_part2.txt")
        .Select(line =>
        {
            var ps = line.Split(':');
            var name = ps[0];
            var outs = ps[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            return new { Name = name, Outs = outs };
        })
        .ToDictionary(x => x.Name, v => v.Outs);
}
