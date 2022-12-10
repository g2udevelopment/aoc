var program = File.ReadAllLines("input.txt");
var cpu = new CPU(Instruction.Parse(program[0]));
Dictionary<int,int> history = new Dictionary<int, int>();
var gpu = new GPU(cpu);

foreach (var line in program[1..])
{
 cpu.AddInstruction(Instruction.Parse(line));   
}

while(cpu.IsRunning()) {
    gpu.Tick();
    history.Add(cpu.CurrentCycle(), cpu.GetX());
    cpu.Tick();
}

//Calculate cycle*currentX every cycle starting at 20 and every 40th cycle.
var sol1 = history.Where((ins) => ins.Key == 20 || ((ins.Key - 20) % 40) == 0).Select(ins => ins.Key * ins.Value).Sum();
System.Console.WriteLine($"Solution1: {sol1}");


System.Console.WriteLine("-----------------Drawing to the screen-------");
System.Console.WriteLine(gpu.GetBuffer());