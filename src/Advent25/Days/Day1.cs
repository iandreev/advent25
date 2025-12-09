namespace Advent25.Days;

internal class Day1
{
    internal void Solve1()
    {
        int zeroes = 0;
        int cur = 50;
        var lines = GetInput();
        foreach (var item in lines)
        {
            var newPos = item.Side switch
            {
                'R' => (cur + item.Number) % 100,
                'L' => MoveLeft(cur, item.Number),
                _ => throw new Exception(),
            };
            //Console.WriteLine($"{cur}: {item.Side} {item.Number}: {newPos}");
            cur = newPos;
            zeroes += cur == 0 ? 1 : 0;
        }

        Console.WriteLine(zeroes);
    }

    internal void Solve2()
    {
        int zeroes = 0;
        int cur = 50;
        var lines = GetInput2();
        foreach (var item in lines)
        {
            if (item.Side == 'R')
            {
                var newPos = cur + item.Number;
                zeroes += newPos / 100;
                cur = newPos % 100;
            }
            else
            {
                zeroes += item.Number / 100;
                var s = cur - item.Number % 100;
                if (s < 0) { s = 100 + s; zeroes += cur == 0 ? 0 : 1; }
                if (s == 0) zeroes += 1;
                cur = s;
            }
        }

        Console.WriteLine(zeroes);
    }

    int MoveLeft(int cur, int num)
    {
        var s = cur - num % 100;
        if (s < 0) s = 100 + s;
        return s;
    }

    List<SafeOperation> GetInput() => File.ReadAllLines("Data/day1.txt")
        .Select(line => new SafeOperation(line[0], int.Parse(line.Substring(1))))
        .ToList();

    List<SafeOperation> GetInput2() => File.ReadAllLines("Data/day1_part2.txt")
        .Select(line => new SafeOperation(line[0], int.Parse(line.Substring(1))))
        .ToList();

    record SafeOperation(char Side, int Number);
}
