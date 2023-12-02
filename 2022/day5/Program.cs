var lines = File.ReadAllLines("input.txt");

var sol1 = ParseFile(lines, ParseMove9000);
System.Console.WriteLine($"Solution1: {sol1}");

var sol2 = ParseFile(lines, ParseMove9001);
System.Console.WriteLine($"Solution2: {sol2}");

static void ParseMove9000(string moveLine, List<List<Char>> stack) {
    var (moves,from,to) = ParseMoveLine(moveLine);

    for (int i = 0; i < moves; i++)
    {
        var toPop = stack[from].First();
        stack[from].Remove(toPop);
        stack[to] = stack[to].Prepend(toPop).ToList();
    }

}

static (int,int,int) ParseMoveLine(string move) {
    var parts = move.Split(' ');
    return (int.Parse(parts[1]), int.Parse(parts[3])-1, int.Parse(parts[5])-1);
}

static void ParseMove9001(string moveLine, List<List<Char>> stack) {
    var (moves,from,to) = ParseMoveLine(moveLine);

    var toPop = stack[from].Take(moves).Reverse().ToList();
    stack[from].RemoveRange(0,moves);
    stack[to].Reverse();
    stack[to].AddRange(toPop);
    stack[to].Reverse();
}

static string ParseFile(string[] lines, Action<string, List<List<char>>> typeFunc)
{
    
    var numberOfStacks = (lines[0].Length-3)/4+1;
    var stack = new List<List<char>>(numberOfStacks);
    // Intialize
    for (int i = 0; i < numberOfStacks; i++)
    {
        stack.Add(new List<char>());
    }
    
    foreach (var line in lines)
    {
        if (line.Count() > 0)
        {
            if (line.Contains('['))
            {
                for (int i = 0; i < numberOfStacks; i++)
                {
                    var crate = line[4 * i + 1];
                    if (crate != ' ')
                    {
                        stack[i].Add(crate);
                    }
                }
            }
            else if (line[0] == 'm')
            { // We are moving
                typeFunc(line, stack);
            }
        }

    }
    return string.Join("", stack.Select(s => s.First()));
}