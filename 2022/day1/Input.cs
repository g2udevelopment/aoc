/// Wraps the input
public class Input
{
    string[] lines;
    public Input(string filename) {
        lines = File.ReadAllLines(filename);
    }

    public List<List<string>> batchBy(string sep) {
{
    var localList = new List<string>();
    var globalList = new List<List<string>>();
    foreach (var line in lines)
    {
        if (line == sep)
        {
            globalList.Add(localList);
            localList = new List<string>();
        }
        else
        {
            localList.Add(line);
        }
    }
    return globalList;
};
}
}