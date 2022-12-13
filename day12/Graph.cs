public record Vertex(int X, int Y);

public class Graph {
    int height,width;
    char[,] map;
    public Vertex End {get;init;}

    public Graph(string[] lines) {
        height = lines.Length;
        width = lines[0].Length;
        map = new char[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
               map[x,y] = lines[y][x];
               if (map[x,y] == 'E') {
                End = new Vertex(x,y);
                System.Console.WriteLine($"End is at: {x},{y}");
               } 
            }
        }
    }

    // Generate all paths with a BFS
    public Dictionary<Vertex,int> BFS(Vertex start) {
        Dictionary<Vertex,int> visited = new Dictionary<Vertex, int>();
        Queue<Vertex> queue = new Queue<Vertex>();
        queue.Enqueue(start);
        visited.Add(start,0);

        while(queue.Count > 0) {
            var current = queue.Dequeue();
            GetNeigbours(current)
            .Where(n => !visited.ContainsKey(n)) //not seen this vertex yet
            .Where(n => (GetHeightFromMap(current) - GetHeightFromMap(n) <= 1) || GetHeightFromMap(current) <= GetHeightFromMap(n)) // Can we jump we go from End to Start
            .ToList()
            .ForEach(n => { 
                visited.Add(n, visited[current]+1);
                queue.Enqueue(n);
                }
                );
        }
        return visited;
    }

    public char GetHeightFromMap(Vertex vertex) {
        var height = map[vertex.X,vertex.Y];
        //System.Console.WriteLine($"height is: {height}");
        height = height switch {
            'S' => 'a',
            'E' => 'z',
            _ => height
        };
        return height ;
    }

    public char GetCharFromMap(Vertex vertex) {
        return map[vertex.X,vertex.Y];
    }

    public List<Vertex> GetNeigbours(Vertex current) {
        return new List<Vertex> {
            new Vertex(current.X, current.Y+1),
            new Vertex(current.X, current.Y-1),
            new Vertex(current.X+1, current.Y),
            new Vertex(current.X-1, current.Y),
        }.Where(v => v.X >= 0 && v.X <= width-1 && v.Y >= 0 && v.Y <=height-1).ToList();
    }

}

