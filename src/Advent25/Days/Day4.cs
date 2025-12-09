using System.Text;

namespace Advent25.Days;

internal class Day4
{
    internal void Solve1()
    {
        var lines = GetInput();

        long result = 0;
        var dirs = new List<(int X, int Y)> {
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, -1),
            (0, 1),
            (1, -1),
            (1, 0),
            (1, 1),
        };

        for (int i = 0; i < lines.Count; i++)
            for (int j = 0; j < lines[i].Length; j++)
            {
                if (At(lines, i, j) == '.')
                    continue;
                var papers = 0;
                foreach (var dir in dirs)
                {
                    papers += At(lines, i + dir.X, j + dir.Y) == '@' ? 1 : 0;
                }
                if (papers < 4)
                    result += 1;
            }
        Console.WriteLine(result);
    }

    char At(List<string> line, int x, int y)
    {
        if (x < 0 || y < 0 || x >= line.Count || y >= line[0].Length)
            return '.';
        return line[x][y];
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        long result = 0;
        var dirs = new List<(int X, int Y)> {
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, -1),
            (0, 1),
            (1, -1),
            (1, 0),
            (1, 1),
        };

        List<(int X, int Y)> vis = new();
        do
        {
            vis = new();

            for (int i = 0; i < lines.Count; i++)
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (At(lines, i, j) == '.')
                        continue;
                    var papers = 0;
                    foreach (var dir in dirs)
                    {
                        papers += At(lines, i + dir.X, j + dir.Y) == '@' ? 1 : 0;
                    }
                    if (papers < 4)
                    {
                        result += 1;
                        vis.Add((i, j));
                    }
                }

            foreach (var pos in vis)
            {
                StringBuilder sb = new(lines[pos.X]);
                sb[pos.Y] = '.';
                lines[pos.X] = sb.ToString();
            }

        } while (vis.Count > 0);
        Console.WriteLine(result);
    }


    List<string> GetInput() => File.ReadAllLines("Data/day4.txt")
        .ToList();

    List<string> GetInput2() => File.ReadAllLines("Data/day4_part2.txt")
        .ToList();
}
