var lines = File.ReadAllLines("input.txt");
var forest = new Forest(lines);

// Function to walk the forest
var funcs = new List<Func<Tree,IEnumerable<int>>>(){forest.WalkWest, forest.WalkEast, forest.WalkNorth, forest.WalkSouth};

var visible = 2*(forest.gridHeight+forest.gridWidth)-4; //Initial visible trees
var scenicScores = new List<int>();

// Walk the grid
for (int col = 1; col < forest.gridWidth-1; col++)
{
    for (int row = 1; row < forest.gridHeight-1; row++)
    {       
        var tree = forest.GetTree(col,row);
        scenicScores.Add(ScenicScore(tree, forest));
        if (IsVisible(tree, forest)) {
            visible++;
        }
    }
}

System.Console.WriteLine($"Solution1: {visible}");
System.Console.WriteLine($"Solution2: {scenicScores.Max()}");

bool IsVisible(Tree tree, Forest forest)
{
    // Visible if not hidden from all direction
    var visible = funcs.Select(f => f(tree).Where(height =>  height >= tree.height).Any()).Select(hidden => hidden ? 1 : 0).Sum() < 4;
    return visible;
}


int ScenicScore(Tree tree, Forest forest)
{
    int score = funcs.Select(f => f(tree).Select((height,idx) => (h: height,i: idx+1)) // Add the index so we know the distance from the start
    .Where((t) => t.h>=tree.height)
    .OrderBy(t => t.i) // Get the first tree that is higher
    .FirstOrDefault((0, f(tree).Count())) // If no tree higher then we can view over the complete line
    .Item2).Aggregate(1,(int x,int y) => x*y);

    return score;
}