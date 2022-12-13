var graph = new Graph(File.ReadAllLines("input.txt"));
var paths = graph.BFS(graph.End);
var lengthToStart = paths.Where(v => graph.GetCharFromMap(v.Key) == 'S').Select(v => v.Value).Single();
System.Console.WriteLine($"Solution1: {lengthToStart}");

var shortestA = paths.Where(v => graph.GetHeightFromMap(v.Key) == 'a').OrderBy(v => v.Value).First().Value;
System.Console.WriteLine($"Solution2: {shortestA}");
