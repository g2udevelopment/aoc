﻿var lines = File.ReadAllLines("input.txt");
var cave = new Cave(lines);

var sol1 = cave.Simulate(new Point(500,0),
(sand,next,bottom) => next.y > bottom,
(sand,next,bottom) => next == sand);
System.Console.WriteLine($"Sol1: {sol1}");


var cave2 = new Cave(lines);
var sol2 = cave2.Simulate(new Point(500,0),
(current,next,bottom) => next == new Point(500,0),
(current,next,bottom) => next == current || (next.y+1 >= bottom + 2));
System.Console.WriteLine($"Sol1: {sol2}");
