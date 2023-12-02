var lines = File.ReadAllLines("input.txt")[1..];//skip the root
var root = new Node(0);
var dirStack = new Stack<Node>();
dirStack.Push(root); //Push the root

ParseFileToTree(lines, dirStack);

var dirList = new List<Node>(); 
var sizeOfRoot = SumTree(root,dirList);

var sol1 = dirList.Where(n => n.Size <= 100000).Sum(n => n.Size);
System.Console.WriteLine($"Solution1: {sol1}");

var spaceNeeded = 30000000 - (70000000 - sizeOfRoot);
var sol2 = dirList.Select(n => n.Size).Where(size => size >= spaceNeeded).Order().First();
System.Console.WriteLine($"Solution2: {sol2}");

static int SumTree(Node node, List<Node> dir) { //DFS
    if (node.IsLeaf())
        return node.Size;
    else {
            var sum = node.Childs.Sum(c => SumTree(c,dir));
            node.Size = sum;
            dir.Add(node); //Maintain a flat list of dirs
            return sum;
            
    }   
}

static (bool,string) IsDirectory(string line) {
    if (line.StartsWith("$ cd")) {
        return (true, line.Split(" ")[2]);
    }
    return (false,"");
}

static (bool,int) IsFile(string line) {
    if (Char.IsNumber(line[0])) {
        return (true, int.Parse(line.Split(" ")[0]));
    }
    return (false,0);
}

static void ParseFileToTree(string[] lines, Stack<Node> dirStack)
{
    foreach (var line in lines)
    {
        var (dir, dirname) = IsDirectory(line);
        if (dir)
        {
            if (dirname == "..")
            {
                var current = dirStack.Pop();
            }
            else
            {
                var current = dirStack.Peek();
                var newdir = new Node(0);
                current.AddNode(newdir);
                dirStack.Push(newdir);
            }
        }
        var (file, size) = IsFile(line);
        if (file)
        {
            var current = dirStack.Peek();
            current.AddNode(new Node(size));
        }
    }
}