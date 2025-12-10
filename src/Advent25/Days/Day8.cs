namespace Advent25.Days;

internal class Day8
{
    internal void Solve1()
    {
        Foo(1000);
    }

    private void Foo(int numberOfConnections)
    {
        var lines = GetInput();

        long result = 0;

        List<HashSet<int>> islands = new();

        List<(int A, int B, double Distance)> distances = new();

        for (int i = 0; i < lines.Count; i++)
            for (int j = i + 1; j < lines.Count; j++)
                distances.Add((i, j, lines[i].Dist(lines[j])));

        distances = distances.OrderBy(s => s.Distance).ToList();

        for (int i = 0; i < numberOfConnections; i++)
        {
            var a = distances[i].A;
            var b = distances[i].B;

            var existinga = islands.Where(isl => isl.Contains(a)).FirstOrDefault();
            var existingb = islands.Where(isl => isl.Contains(b)).FirstOrDefault();
            if (existinga != null && existingb != null)
            {
                if (existinga != existingb)
                {
                    existingb.ToList().ForEach(x => existinga.Add(x));
                    existinga.Add(b);

                    islands.Remove(existingb);
                }
            }
            else if (existinga != null)
            {
                existinga.Add(a);
                existinga.Add(b);
            }
            else if (existingb != null)
            {
                existingb.Add(a);
                existingb.Add(b);
            }
            else
            {
                islands.Add([a, b]);
            }
        }

        result = islands.OrderByDescending(x => x.Count).Take(3).Select(x => x.Count).Aggregate((x, y) => x * y);
        Console.WriteLine(result);
    }

    internal void Solve2()
    {
        var lines = GetInput2();

        double result = 0;

        List<HashSet<int>> islands = new();

        List<(int A, int B, double Distance)> distances = new();

        for (int i = 0; i < lines.Count; i++)
            for (int j = i + 1; j < lines.Count; j++)
                distances.Add((i, j, lines[i].Dist(lines[j])));

        distances = distances.OrderBy(s => s.Distance).ToList();

        for (int i = 0; i < distances.Count; i++)
        {
            var a = distances[i].A;
            var b = distances[i].B;

            var existinga = islands.Where(isl => isl.Contains(a)).FirstOrDefault();
            var existingb = islands.Where(isl => isl.Contains(b)).FirstOrDefault();
            if (existinga != null && existingb != null)
            {
                if (existinga != existingb)
                {
                    existingb.ToList().ForEach(x => existinga.Add(x));
                    existinga.Add(b);

                    islands.Remove(existingb);
                }
            }
            else if (existinga != null)
            {
                existinga.Add(a);
                existinga.Add(b);
            }
            else if (existingb != null)
            {
                existingb.Add(a);
                existingb.Add(b);
            }
            else
            {
                islands.Add([a, b]);
            }

            if (islands.Count == 1 && islands.Select(x => x.Count).Sum() == lines.Count)
            {
                result = (double)lines[a].X * lines[b].X;
                break;
            }
        }

        Console.WriteLine(result);
    }


    List<Box> GetInput() => File.ReadAllLines("Data/day8.txt")
        .Select(line =>
        {
            var a = line.Split(',').Select(int.Parse).ToList();
            return new Box(a[0], a[1], a[2]);
        })
        .ToList();

    List<Box> GetInput2() => File.ReadAllLines("Data/day8_part2.txt")
        .Select(line =>
        {
            var a = line.Split(',').Select(int.Parse).ToList();
            return new Box(a[0], a[1], a[2]);
        })
        .ToList();

    record Box(int X, int Y, int Z)
    {
        public double Dist(Box other)
        {
            var x = Math.Pow(X - other.X, 2);
            var y = Math.Pow(Y - other.Y, 2);
            var z = Math.Pow(Z - other.Z, 2);
            return Math.Sqrt(x + y + z);
        }
    }
}
