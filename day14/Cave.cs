public enum Substance
{
    AIR,
    BLOCK
}
public record Point(int x, int y); //0,0 top left

public class Cave {
    Dictionary<Point, Substance> map = new Dictionary<Point, Substance>();
    int bottom = 0;

    public Cave(string[] lines) {
        var traces = lines
        .Select(l => ParsePoints(l.Split("->",StringSplitOptions.TrimEntries)));
        foreach (var trace in traces)
        {
            for (int i = 0; i < trace.Count()-1; i++)
            {
                CreateLine(trace.ElementAt(i),trace.ElementAt(i+1));
            }
        }
    }

    public int Simulate(Point start, Func<Point,Point,int,bool> end, Func<Point,Point,int,bool> notMoving) {
        int units = 0;
        bool running = true;
        var current = new Point(start.x, start.y);
        while (running) {  
            var next = NextPoint(current);
            
            if (end(current,next,bottom)) {
                running = false;
            }
            if (notMoving(current,next,bottom)) { //Sitting still
                map.Add(next,Substance.BLOCK);
                next = new Point(start.x, start.y);
                units++;
            }
            current = next;
        }
        return units;
    }

    public Substance GetSubstance(Point sand){
        if (map.ContainsKey(sand)) {
            return Substance.BLOCK;
        } else {
            return Substance.AIR;
        }
    }

    public Point NextPoint(Point sand) {
        if (GetSubstance(new Point(sand.x, sand.y+1)) == Substance.AIR) {
            return new Point(sand.x, sand.y+1);
        } else if (GetSubstance(new Point(sand.x-1, sand.y+1)) == Substance.AIR) {
            return new Point(sand.x-1, sand.y+1);
        } else if (GetSubstance(new Point(sand.x+1, sand.y+1)) == Substance.AIR) {
            return new Point(sand.x+1, sand.y+1);
        } else {
            return sand;
        }
    }

    public Dictionary<Point,Substance> GetMap() => map;

    private IEnumerable<Point> ParsePoints(string[] points)
    {
        //.Console.WriteLine(points.Count());
        return points.Select(p => new Point(
        int.Parse(p.Split(',')[0]), 
        int.Parse(p.Split(',')[1])));
    }

    private void CreateLine(Point one, Point two) {
        int dx = two.x - one.x;
        int dy = two.y - one.y;
        int length = Math.Max(Math.Abs(dx),Math.Abs(dy))+1;
        bottom = Math.Max(bottom, Math.Max(one.y, two.y)); //determine bottom
        for (int i = 0; i < length; i++)
        {
            var p = new Point(one.x + Math.Clamp(dx,-1,1)*i, one.y + Math.Clamp(dy,-1,1)*i);
            if (!map.ContainsKey(p)) {
                
                map.Add(p, Substance.BLOCK);
            }
        }
    }
}