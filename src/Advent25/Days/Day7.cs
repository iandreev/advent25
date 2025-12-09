namespace Advent25.Days;

internal class Day7
{
    internal void Solve1()
    {
        var lines = GetInput();

        long result = 0;

        HashSet<int> indexes = [lines[0].IndexOf('S')];

        for (int i = 1; i < lines.Count; i++)
        {
            HashSet<int> newIndexes = new();
            foreach (var idx in indexes)
            {
                if (lines[i][idx] == '^')
                {
                    result++;
                    newIndexes.Add(idx - 1);
                    newIndexes.Add(idx + 1);
                }
                else
                {
                    newIndexes.Add(idx);
                }
            }

            indexes = newIndexes;
        }


        Console.WriteLine(result);
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        long result = Foo(lines, 1, lines[0].IndexOf('S'));

        Console.WriteLine(result);
    }

    Dictionary<(int Level, int Index), long> mem = new();
    long Foo(List<string> map, int level, int index)
    {
        if (mem.ContainsKey((level, index)))
            return mem[(level, index)];
        if (level == map.Count - 1) return 1;

        if (map[level][index] == '^')
        {
            var left = Foo(map, level + 1, index - 1);
            var right = Foo(map, level + 1, index + 1);

            mem[(level + 1, index - 1)] = left;
            mem[(level + 1, index + 1)] = right;
            return left + right;
        }
        else
        {
            var res = Foo(map, level+1, index);
            mem[(level + 1, index)] = res;
            return res;
        }
    }


    List<string> GetInput() => File.ReadAllLines("Data/day7.txt")
        .ToList();

    List<string> GetInput2() => File.ReadAllLines("Data/day7_part2.txt")
        .ToList();
}
