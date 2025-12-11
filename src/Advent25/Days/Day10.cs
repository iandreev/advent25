using System.Diagnostics;
using System.Text;

namespace Advent25.Days;

internal class Day10
{
    internal void Solve1()
    {
        var lines = GetInput();

        double result = lines.Select(FixMachine).Sum();

        Console.WriteLine(result);
    }

    int FixMachine(Data m)
    {
        map = new();
        var state = m.States.Replace("#", ".");
        var result = Foo(state, m, 0);
        return result;
    }

    Dictionary<string, int> map;
    int Foo(string state, Data m, int clicks)
    {
        if (clicks > 6)
            return int.MaxValue;
        if (map.ContainsKey(state) && map[state] < clicks)
            return int.MaxValue;
        map[state] = clicks;

        if (state == m.States)
            return clicks;

        var result = int.MaxValue;
        for (int i = 0; i < m.Buttons.Count; i++)
        {
            StringBuilder sb = new StringBuilder(state);
            foreach (var button in m.Buttons[i])
            {
                sb[button] = sb[button] == '.' ? '#' : '.';
            }
            result = Math.Min(result, Foo(sb.ToString(), m, clicks + 1));
        }

        return result;
    }

    internal void Solve2()
    {
        var lines = GetInput2();
        double result = lines.Select(FixMachine2).Sum();

        Console.WriteLine(result);
    }

    int FixMachine2(Data2 m)
    {
        var state = new int[m.Reqs.Count];

        var cw = new Stopwatch();
        cw.Start();
        var result = Foo2(state, m, 0);
        cw.Stop();
        Console.WriteLine($"res: {result} in {cw.Elapsed}");
        return result;
    }

    bool Eq(int[] state, Data2 m)
    {
        for (int i = 0; i < state.Length; i++)
            if (state[i] != m.Reqs[i])
                return false;
        return true;
    }
    int Foo2(int[] state, Data2 m, int clicks)
    {
        //if (Eq(state, m))
        //    return clicks;

        var result = int.MaxValue;
        for (int i = 0; i < m.Buttons.Count; i++)
        {
            int[] newState = new int[state.Length];
            Array.Copy(state, newState, state.Length);

            var tryIt = true;
            var match = 0;

            for (int b = 0; b < m.Reqs.Count; b++)
            {
                if (m.Buttons[i].Contains(b))
                    newState[b]++;

                if (newState[b] == m.Reqs[b]) match++;
                if (newState[b] > m.Reqs[b])
                {
                    tryIt = false;
                    break;
                }
            }

            if (match == m.Reqs.Count)
                return clicks + 1;
            if (tryIt)
                result = Math.Min(result, Foo2(newState, m, clicks + 1));
        }

        return result;
    }

    List<Data> GetInput() => File.ReadAllLines("Data/day10.txt")
        .Select(line =>
        {
            var ps = line.Split(']');
            var state = ps[0].Replace("[", "");
            var p3 = ps[1].Split('{');

            var jo = p3[1].Replace("}", "").Split(',').Select(int.Parse).ToList();

            var pbuttons = p3[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var buttons = pbuttons.Select(b =>
                b.Replace("(", "").Replace(")", "").Split(',').Select(int.Parse).ToList()).ToList();

            return new Data(state, buttons, jo);
        })
        .ToList();

    List<Data2> GetInput2() => File.ReadAllLines("Data/day10_part2.txt")
        .Select(line =>
        {
            var ps = line.Split(']');
            var state = ps[0].Replace("[", "");
            var p3 = ps[1].Split('{');

            var jo = p3[1].Replace("}", "").Split(',').Select(int.Parse).ToList();

            var pbuttons = p3[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var buttons = pbuttons.Select(b =>
                b.Replace("(", "").Replace(")", "").Split(',').Select(int.Parse).ToHashSet())
            .OrderByDescending(x => x.Count)
            .ToList();

            return new Data2(state, buttons, jo);
        })
        .ToList();

    record Data(string States, List<List<int>> Buttons, List<int> Reqs);
    record Data2(string States, List<HashSet<int>> Buttons, List<int> Reqs);

}
