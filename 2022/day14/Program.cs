var lines = File.ReadAllLines("input.txt");
var cave = new Cave(lines);

var sol1 = cave.Simulate(new Point(500,0),
(_,next,bottom) => next.y > bottom,
(current,next,bottom) => next == current);
System.Console.WriteLine($"Sol1: {sol1}");


var cave2 = new Cave(lines);
var sol2 = cave2.Simulate(new Point(500,0),
(_,next,_) => next == new Point(500,0),
(current,next,bottom) => next == current || (next.y+1 >= bottom + 2)); //You should not be adding a not INF line
System.Console.WriteLine($"Sol1: {sol2}");
