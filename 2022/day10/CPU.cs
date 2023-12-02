public class CPU {
    int X = 1;
    Dictionary<int,int> history = new Dictionary<int, int>();
    Queue<Instruction> instructions = new Queue<Instruction>();
    Instruction current_ins;
    int cycle = 1;
    bool running = true;

    public void RegisterX(int value) {
        X += value;
    }

    public int GetX() => X;
    public int CurrentCycle() => cycle;

    public CPU(Instruction first) {
        current_ins = first;
    }

    public void AddInstruction(Instruction ins) {
        this.instructions.Enqueue(ins);
    }

    public bool IsRunning() {
        return this.running;
    }

    public void Tick() {
         // Mark value of X
        current_ins.Tick();   
        if (current_ins.ShouldProcess()) {// Should we already process the instruction
            current_ins.Process(this);
            if (instructions.Count() > 0) {
                current_ins = Next();
            } else {
                this.running = false;
            }
        }
        
        cycle++;
    }

    public Instruction Next() {
        return instructions.Dequeue();
    }

    public Dictionary<int,int> GetHistory() => this.history;
}