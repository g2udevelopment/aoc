using System.Text.Json;

var packets = File.ReadAllLines("input.txt").Where(l => l != "");
var chunks = packets.Chunk(2);

var sol1 = chunks
.Select((chunk,idx) => (score: CompareChunk(chunk),idx: idx+1))
.Where(chunk => chunk.score  == -1)
.Sum(chunk => chunk.idx);
System.Console.WriteLine($"Solution1: {sol1}");

var p = packets.ToList();
p.AddRange(new List<string>(){"[[2]]","[[6]]"});
var sol2 = p
.Select(packet => JsonDocument.Parse(packet).RootElement)
.OrderBy<JsonElement,JsonElement>((key) => key, Comparer<JsonElement>.Create((c1,c2) => Compare(c1,c2)))
.Select((chunk,idx) => (chunk: chunk,idx: idx+1))
.Where(chunk => chunk.chunk.ToString() == "[[2]]" || chunk.chunk.ToString() == "[[6]]")
.Select(chunk => chunk.idx)
.Aggregate(1, (a,b) => a*b);
System.Console.WriteLine($"Solution2: {sol2}");


static int CompareChunk(string[] chunk) {
    var left = JsonDocument.Parse(chunk[0]).RootElement;
    var right = JsonDocument.Parse(chunk[1]).RootElement;
    return Compare(left,right);
}

 static int Compare(JsonElement left, JsonElement right) {
    int comp = (left.ValueKind, right.ValueKind) switch 
    {
        (JsonValueKind.Number,JsonValueKind.Number) => left.GetInt32().CompareTo(right.GetInt32()),
        (JsonValueKind.Array, JsonValueKind.Array) => ArrayCompare(left.EnumerateArray(),right.EnumerateArray()),
        (JsonValueKind.Array, JsonValueKind.Number) => ArrayCompare(left.EnumerateArray(),JsonDocument.Parse($"[{right.ToString()}]").RootElement.EnumerateArray()),
        (JsonValueKind.Number, JsonValueKind.Array) => ArrayCompare(JsonDocument.Parse($"[{left.ToString()}]").RootElement.EnumerateArray(),right.EnumerateArray()),
        (_,_) => throw new ArgumentException("bad input")

    };
    return comp;
}

static int ArrayCompare(JsonElement.ArrayEnumerator left, JsonElement.ArrayEnumerator right) {
var lnext= left.MoveNext();
var rnext = right.MoveNext();
if (!lnext && !rnext) return 0;    
if (!lnext) return -1;
if (!rnext) return 1;
var comp = Compare(left.Current, right.Current);
if (comp == 0) {
    return ArrayCompare(left,right);
}
return comp;
}

