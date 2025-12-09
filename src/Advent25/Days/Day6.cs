namespace Advent25.Days;

internal class Day6
{
    internal void Solve1()
    {
        var lines = GetInput();

        long result = 0;


        for (int i = 0; i < lines[0].Count; i++)
        {
            var op = lines[^1][i];
            var items = lines.Take(lines.Count - 1).Select(x => x[i]);

            if (op == "+") result += items.Select(long.Parse).Sum();
            if (op == "*") result += items.Select(long.Parse).Aggregate((x, y) => x * y);
        }

        Console.WriteLine(result);
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        long result = 0;


        for (int i = 0; i < lines[0].Count; i++)
        {
            var op = lines[^1][i];
            var items = lines.Take(lines.Count - 1).Select(x => x[i]).ToList();
            var maxlen = items.Select(x => x.Length).Max();
            items = items.Select(x => x.PadLeft(maxlen, '0')).ToList();

            var nums = new List<string>();
            for (int j = 0; j < maxlen; j++)
            {
                var s = "";
                for (int k = 0; k < items.Count; k++)
                {
                    s += items[k][j];
                }
                nums.Add(s);
            }


            if (op == "+") result += nums.Select(long.Parse).Sum();
            if (op == "*") result += nums.Select(long.Parse).Aggregate((x, y) => x * y);
        }

        Console.WriteLine(result);
    }


    List<List<string>> GetInput() => File.ReadAllLines("Data/day6.txt")
        .Select(line =>
            line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList())
        .ToList();

    List<List<string>> GetInput2()
    {
        var lines = File.ReadAllLines("Data/day6_part2.txt");
        var ops = lines[^1];

        var result = new List<List<string>>();
        for (int i = 0; i < lines.Length; i++)
        {
            result.Add(new List<string>());
        }

        int start = 0;
        var lastOps = ops[0].ToString();
        for (int i = 1; i < ops.Length; i++)
        {
            if (ops[i] != ' ')
            {
                for (int j = 0; j < result.Count - 1; j++)
                {
                    result[j].Add(lines[j].Substring(start, i-start-1));
                }
                result[^1].Add(lastOps);

                lastOps = ops[i].ToString();
                start = i;
            }
        }

        for (int j = 0; j < result.Count - 1; j++)
        {
            result[j].Add(lines[j].Substring(start));
        }
        result[^1].Add(lastOps);


        return result;
    }
}
