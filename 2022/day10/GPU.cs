// X register is middle of sprite ### , starting at 0
public class GPU {
    const int WIDTH = 40;
    const int HEIGHT = 6;
    (int x,int y) pos = (0,0);
    string buffer = "";
    CPU cpu;

    public GPU(CPU cpu) {
        this.cpu = cpu;
    }
    public void Tick () {
        drawCurrentPos();
        if (pos.x + 1 == WIDTH) {//Reset
            buffer+='\n';
            pos.y++;
            pos.x = 0;
        } else {
            pos.x++;
        }
    }

    private void drawCurrentPos()
    {
        var x = cpu.GetX(); // x 1 x
        if (pos.x >= x-1 && pos.x <= x+1) {
            buffer += "#";
        } else {
            buffer += ".";
        }
        
    }

    public string GetBuffer() => this.buffer;
}