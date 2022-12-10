public abstract class Instruction {
    protected int cycles;
    int currentCycle;

    public static Instruction Parse(string line) {
        if (line.StartsWith("addx")){
            int arg = int.Parse(line.Split(" ")[1]);
            return new AddInstruction(arg);
        }
        return new NopInstruction();
    }
    public bool ShouldProcess() {
        return currentCycle == cycles;
    }

    public void Tick() {
        currentCycle++;
    }
    public abstract void Process(CPU cpu);
}

public class AddInstruction : Instruction {
    int arg;
    public AddInstruction(int arg) {
        this.arg = arg;
        base.cycles = 2;
    }

    public override void Process(CPU cpu)
    {
        cpu.RegisterX(arg);
    }
}

public class NopInstruction : Instruction {
    public NopInstruction() {
        base.cycles = 1;
    }

    public override void Process(CPU cpu)
    {
        return;
    }
}

public class EmptyInstruction : Instruction {
    public EmptyInstruction() {
        base.cycles = 0;
    }

    public override void Process(CPU cpu)
    {
        return;
    }
}

