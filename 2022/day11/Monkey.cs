public class Monkey {
    public Monkey(string chunk)
    {
        var lines = chunk.Split("\n");
        Number = int.Parse(lines[0].Split(" ")[1][0].ToString());
        Divisor = int.Parse(lines[3].Split(" ").Last());
        TrueMonkey = int.Parse(lines[4].Split(" ").Last());
        FalseMonkey = int.Parse(lines[5].Split(" ").Last());
        Items = lines[1][17..].Split(", ").Select(item => long.Parse(item)).ToList();
        if (lines[2].Split(" ").Last() == "old") {
            Operation = (old) => old * old;
        } else {
            var op = lines[2].Split(" ")[6];
            var arg = int.Parse(lines[2].Split(" ")[7]);
            Operation = op == "*" ? (old) => old * arg : (old) => old + arg; 
        } 
    }

    int Number;
    public List<long> Items {get;}
    Func<long,long> Operation;
    public int Divisor {get;}
    int TrueMonkey;
    int FalseMonkey;
    public ulong Inspects {get;private set;}

    public IEnumerable<(int monkey, long value)> Throw(int mod) {
        return Items.Select(item => Calculate(item,mod));
    }

    private (int monkey, long value) Calculate(long item, int mod) {
        Inspects++;
        long value = mod == 0 ? Operation(item) / 3 :  Operation(item) % mod;
        if ( (value % Divisor) == 0L) {
            return (TrueMonkey, value);
        } else {
            return (FalseMonkey, value);
        }
    }

}