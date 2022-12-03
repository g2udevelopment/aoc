var lines = File.ReadAllLines("input.txt");

int sumSolution1 = SolutionOne(lines);

System.Console.WriteLine($"Solution1: {sumSolution1}");

int sumSolution2 = SolutionTwo(lines);

System.Console.WriteLine($"Solution2: {sumSolution2}");

static int CalculatePrio(char item)
{
    return item > 'Z' ? item - 'a' + 1 : item - 'A' + 27;
}

static int SolutionOne(string[] lines)
{
    var sumSolution1 = 0;

    foreach (var line in lines)
    {
        var characters = new HashSet<char>();
        var midpoint = line.Length / 2;
        var left = line.Substring(0,midpoint);
        var right = line.Substring(midpoint);
        sumSolution1 += CalculatePrio(left.Intersect(right).Single());
    }

    return sumSolution1;
}

static int SolutionTwo(string[] lines)
{
    var elves = lines.Chunk(3);
    var sumSolution2 = elves.
        Select(elve => CalculatePrio(elve[0]
        .Intersect(elve[1])
        .Intersect(elve[2]).Single())).Sum();
    return sumSolution2;
}