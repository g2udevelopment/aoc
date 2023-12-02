using System.IO;

var input = new Input("test.txt");
var calsPerDwarf = input.batchBy("");

// Solution1
var highest = CalcPerDwarf(calsPerDwarf).Take(1).Single();
System.Console.WriteLine($"Solution to problem1: {highest} ");

// Solution 2
var highest3 = CalcPerDwarf(calsPerDwarf).Take(3).Sum();
System.Console.WriteLine($"Solution problem2: {highest3}");

static IOrderedEnumerable<int> CalcPerDwarf(List<List<string>> calsPerDwarf)
{
    return calsPerDwarf.Select(dwarf => dwarf.Select(c => int.Parse(c)).Sum()).OrderDescending();
}