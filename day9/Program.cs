var lines = File.ReadAllLines("input.txt");

HashSet<(int, int)> visited = new HashSet<(int, int)>();
visited.Add((0, 0)); // start with initial pos visitied

//var knot1 = (0,0);
var knot2 = (0, 0);



static List<(int, int)> createRope(int knots)
{
    var rope = new List<(int, int)>();
    for (int i = 0; i < knots; i++)
    {
        rope.Add((0, 0));
    }
    return rope;
}
var rop1 = createRope(10);

foreach (var line in lines)
{
    var split = line.Split(" ");
    
    var times = int.Parse(split[1]);
    for (int i = 0; i < times; i++)
    {
        //var head = rop1.Take(1).Single();
        //head = Move(head, dir);
        // var tail = rop1.Skip(1);
        var dir = ParseMove(split[0]);
        var index = 0;
        var newrope = new List<(int,int)>();
        foreach (var head in rop1) // Move a knots
        {
            index++;//1
            //Move(head, dir);
            var newhead = Move(head, dir);//move dir
            newrope.Add(newhead); //1 in newrop
            System.Console.WriteLine(newhead);
            if (index < rop1.Count())
            {
                System.Console.WriteLine($"index: {index}");
                var tail = rop1.Skip(index).First();
                var delta = Delta(newhead, tail);
                if (toFar(delta))
                {
                    dir = Clamp(delta);
                    //knot2 = Move(knot2,tailmove);
                } else {
                    dir = (0,0);
                }
            }
        }
        rop1 = newrope;
        visited.Add(rop1.Last());
        System.Console.WriteLine($"Tail now at: {rop1.Last()}");
    }
}

System.Console.WriteLine($"Tail visisted: {visited.Count()}");

bool toFar((int, int) delta)
{
    return Math.Abs(delta.Item1) > 1 || Math.Abs(delta.Item2) > 1;
}

static (int, int) ParseMove(string direction)
{
    return direction switch
    {
        "R" => (1, 0),
        "L" => (-1, 0),
        "U" => (0, 1),
        "D" => (0, -1),
        _ => throw new ArgumentException()
    };
}

static (int, int) Move((int, int) point, (int, int) direction)
{
    return (point.Item1 + direction.Item1, point.Item2 + direction.Item2);
}

static (int, int) Delta((int, int) head, (int, int) tail)
{
    return (head.Item1 - tail.Item1, head.Item2 - tail.Item2);
}

static (int, int) Clamp((int, int) point)
{
    return (Math.Clamp(point.Item1, -1, 1), Math.Clamp(point.Item2, -1, 1));
}
