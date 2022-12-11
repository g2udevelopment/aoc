// See https://aka.ms/new-console-template for more informatio
var chunks = File.ReadAllText("input.txt").Split("\n\n");
Solution1(chunks);
Solution2(chunks);


static void Solution1(string[] chunks)
{
    const int ROUNDS = 20;
    var monkeys = chunks.Select((chunk, idx) => (idx, monkey: new Monkey(chunk))).ToDictionary(m => m.idx, m => m.monkey);
    var nrmonkeys = monkeys.Count();
    int mod = 0;

    DoBusiness(monkeys, ROUNDS, nrmonkeys, mod);

    var sol1 = monkeys.Values.OrderByDescending(monkey => monkey.Inspects).Take(2).Aggregate(1UL, (acc, m) => acc * m.Inspects);
    System.Console.WriteLine($"Solution1: {sol1}");

}

static void Solution2(string[] chunks)
{
    const int ROUNDS = 10_000;
    var monkeys = chunks.Select((chunk, idx) => (idx, monkey: new Monkey(chunk))).ToDictionary(m => m.idx, m => m.monkey);
    var nrmonkeys = monkeys.Count();
    // We use the product of all divisors as a mod for all arthimetic
    int mod = monkeys.Values.Aggregate(1, (acc, m) => acc * m.Divisor);

    DoBusiness(monkeys, ROUNDS, nrmonkeys, mod);

    var sol2 = monkeys.Values.OrderByDescending(monkey => monkey.Inspects).Take(2).Aggregate(1UL, (acc, m) => acc * m.Inspects);
    System.Console.WriteLine($"Solution2: {sol2}");
}


static void DoBusiness(Dictionary<int, Monkey> monkeys, int ROUNDS, int nrmonkeys, int mod)
{
    for (int i = 0; i < ROUNDS; i++)
    {
        for (int j = 0; j < nrmonkeys; j++)
        {
            foreach (var item in monkeys[j].Throw(mod))
            {
                monkeys[item.monkey].Items.Add(item.value);
            }
            monkeys[j].Items.Clear();
        }
    }
}