var lines = File.ReadAllLines("input.txt");

var solution1 = lines
.Select(line => (line.Split(",")[0], line.Split(",")[1]))
.Select(group => (RangeContainsComplete(ToRange(group.Item1), ToRange(group.Item2))))
.Count(p => p == true);

System.Console.WriteLine($"Solution 1 is: {solution1}");

var solution2 = lines
.Select(line => (line.Split(",")[0], line.Split(",")[1]))
.Select(group => (RangeContainsPartial(ToRange(group.Item1), ToRange(group.Item2))))
.Count(p => p == true);

System.Console.WriteLine($"Solution 2 is: {solution2}");


static Range ToRange(string range) {
    var r = range.Split("-");
    return int.Parse(r[0])..int.Parse(r[1]);
}

static bool RangeContainsComplete(Range fst, Range snd) {
    return (fst.Start.Value >= snd.Start.Value && fst.End.Value <= snd.End.Value) ||
    (snd.Start.Value >= fst.Start.Value && snd.End.Value <= fst.End.Value);
}

static bool RangeContainsPartial(Range fst, Range snd) {
   if (fst.Start.Value <= snd.Start.Value ) 
    return !(fst.Start.Value < snd.Start.Value && fst.End.Value < snd.Start.Value);
   return !(snd.Start.Value < fst.Start.Value && snd.End.Value < fst.Start.Value);

}