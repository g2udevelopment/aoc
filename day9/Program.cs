var lines = File.ReadAllLines("input.txt");

Solution(lines,2);
Solution(lines,10);

bool toFar((int x, int y) delta)
{
    return Math.Abs(delta.x) > 1 || Math.Abs(delta.y) > 1;
}

static (int, int) ParseMove(char direction)
{
    return direction switch
    {
        'R' => (1, 0),
        'L' => (-1, 0),
        'U' => (0, 1),
        'D' => (0, -1),
        _ => throw new ArgumentException()
    };
}

static (int, int) Move((int x, int y) point, (int x, int y) direction)
{
    return (point.x + direction.x, point.y + direction.y);
}

static (int, int) Delta((int x, int y) head, (int x, int y) tail)
{
    return (head.x - tail.x, head.y - tail.y);
}

static (int, int) Clamp((int x, int y) point)
{
    return (Math.Clamp(point.x, -1, 1), Math.Clamp(point.y, -1, 1));
}

List<(int, int)> ParseRope(string[] lines, HashSet<(int, int)> visited, List<(int, int)> rope)
{
    foreach (var line in lines)
    {
        var times = int.Parse(line[2..]);
        for (int i = 0; i < times; i++)
        {
            var dir = ParseMove(line[0]);
            var index = 0;
            var newrope = new List<(int, int)>();
            foreach (var knot in rope) // Move a knots
            {
                index++;//1
                var newknot = Move(knot, dir);//move dir
                newrope.Add(newknot); //1 in newrop
                if (index < rope.Count())
                {
                    var knotbehind = rope.Skip(index).First();
                    var delta = Delta(newknot, knotbehind);
                    dir = toFar(delta) ? Clamp(delta) : (0, 0);
                }
            }
            rope = newrope;
            visited.Add(rope.Last());
        }
    }

    return rope;
}

static List<(int, int)> createRope(int knots)
{
    var rope = new List<(int, int)>();
    for (int i = 0; i < knots; i++)
    {
        rope.Add((0, 0));
    }
    return rope;
}

void Solution(string[] lines, int knots)
{
    HashSet<(int, int)> visited = new HashSet<(int, int)>();
    visited.Add((0, 0)); // start with initial pos visitied
    ParseRope(lines, visited, createRope(knots));
    System.Console.WriteLine($"Sol1 visisted: {visited.Count()}");
}