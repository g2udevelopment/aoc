public record Tree(int height, int row, int col);

public class Forest
{
    int[,] grid;
    public int gridWidth, gridHeight;

    public Forest(string[] gridLines)
    {
        parseForestGrid(gridLines);
    }

    private void parseForestGrid(string[] lines)
    {
        gridWidth = lines[0].Length;
        gridHeight = lines.Length;

        grid = new int[gridWidth, gridHeight];

        for (int col = 0; col < gridWidth; col++)
        {
            for (int row = 0; row < gridHeight; row++)
            {
                grid[col, row] = int.Parse(lines[row][col].ToString());
            }
        }
    }

    public Tree GetTree(int col, int row) {
        return new Tree(grid[col,row], row,col);
    }

    public IEnumerable<int> WalkWest(Tree tree)
    {
        for (int i = tree.col - 1; i >= 0; i--) //W
        {
            var neighbour = grid[i, tree.row];
            yield return neighbour;

        }
    }

    public IEnumerable<int> WalkEast(Tree tree)
    {
        for (int i = tree.col + 1; i < gridWidth; i++) //E
        {
            var neighbour = grid[i, tree.row];
            yield return neighbour;
        }
    }

    public IEnumerable<int> WalkNorth(Tree tree)
    {
        for (int i = tree.row - 1; i >= 0; i--) // N
        {
            var neighbour = grid[tree.col, i];
            yield return neighbour;
        }
    }

    public IEnumerable<int> WalkSouth(Tree tree)
    {
        for (int i = tree.row + 1; i < gridHeight; i++) //S
        {
            var neighbour = grid[tree.col, i];
            yield return neighbour;
        }
    }
}