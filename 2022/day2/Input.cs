/// Wraps the input
public class Input
{
    string[] lines;
    public Input(string filename) {
        lines = File.ReadAllLines(filename);
    }

    public List<(string,string)> toTuple() {
        var result = new List<(string,string)>();
        foreach (var line in lines)
        {
            var tokens = line.Split(" ");
            result.Add((tokens[0], tokens[1]));
        }
        return result;
    }
}