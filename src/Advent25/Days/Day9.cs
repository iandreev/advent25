using System.Text;

namespace Advent25.Days;

internal class Day9
{
    internal void Solve1()
    {
        var lines = GetInput();

        double result = 0;

        for (int i = 0; i < lines.Count; i++)
            for (int j = 0; j < lines.Count; j++)
            {
                var a = Math.Abs(lines[i].X - lines[j].X) + 1d;
                var b = Math.Abs(lines[i].Y - lines[j].Y) + 1d;

                result = Math.Max(result, a * b);
            }

        Console.WriteLine(result);
    }

    internal void Solve2()
    {
        var points = GetInput2();
        points.Add(points[0]);

        var map = new byte[1000, 1000];
        for (int i = 1; i < points.Count; i++)
        {
            var x2 = points[i].X / 100;
            var y2 = points[i].Y / 100;

            var x1 = points[i - 1].X / 100;
            var y1 = points[i - 1].Y / 100;

            for (var xx = Math.Min(x1,x2); xx <= Math.Max(x1,x2); xx++)
                for (var yy = Math.Min(y1,y2); yy <= Math.Max(y1,y2); yy++)
                    map[xx, yy] = 2;
            map[x1, y1] = 1;
            map[x2, y2] = 1;
        }
        Queue<(int X, int Y)> q = new Queue<(int X, int Y)>();
        List<(int Dx, int Dy)> dirs = [(-1, 0), (1, 0), (0, -1), (0, 1)];
        q.Enqueue((0, 0));
        do
        {
            var p = q.Dequeue();
            map[p.X, p.Y] = 7;

            foreach (var d in dirs)
            {
                var nx = p.X + d.Dx;
                var ny = p.Y + d.Dy;
                if (nx >= 0 && ny >= 0 && nx < 1000 && ny < 1000 && map[nx, ny] == 0)
                {
                    map[nx, ny] = 7;
                    q.Enqueue((nx, ny));
                }

            }

        } while (q.Count > 0);
        for (int i = 0; i < 1000; i++)
            for (int j = 0; j < 1000; j++)
                if (map[i, j] == 0) map[i, j] = 2;


        //StringBuilder sb = new();
        //for (int i = 0; i < 1000; i++)
        //{
        //    for (int j = 0; j < 1000; j++)
        //        sb.Append(map[i, j]);
        //    sb.AppendLine();
        //}
        //File.WriteAllText("map.txt", sb.ToString());


        double result = 0;

        for (int i = 0; i < points.Count; i++)
            for (int j = 0; j < points.Count; j++)
            {
                var a = Math.Abs(points[i].X - points[j].X) + 1d;
                var b = Math.Abs(points[i].Y - points[j].Y) + 1d;

                if (a * b > result)
                {
                    var x1 = Math.Min(points[i].X, points[j].X) / 100;
                    var x2 = Math.Max(points[i].X, points[j].X) / 100;
                    var y1 = Math.Min(points[i].Y, points[j].Y) / 100;
                    var y2 = Math.Max(points[i].Y, points[j].Y) / 100;

                    if (IsInside(map, x1, x2, y1, y2))
                    {
                        result = a * b;
                    }

                }
            }

        Console.WriteLine(result);
    }

    private static bool IsInside(byte[,] map, long x1, long x2, long y1, long y2)
    {
        for (var x = x1; x <= x2; x++)
            for (var y = y1; y <= y2; y++)
            {
                if (map[x, y] != 1 && map[x, y] != 2) return false;
            }

        return true;
    }

    List<Point> GetInput() => File.ReadAllLines("Data/day9.txt")
        .Select(line =>
        {
            var a = line.Split(',').Select(int.Parse).ToList();
            return new Point(a[0], a[1]);
        })
        .ToList();

    List<Point> GetInput2() => File.ReadAllLines("Data/day9_part2.txt")
        .Select(line =>
        {
            var a = line.Split(',').Select(int.Parse).ToList();
            return new Point(a[0], a[1]);
        })
        .ToList();

    record Point(long X, long Y);
}
