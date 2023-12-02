public record Cell(int value, int row, int col);

public class Grid2D
{
    int[,] grid;
    public int gridWidth, gridHeight;

    public Grid2D(string[] gridLines)
    {
        gridWidth = gridLines[0].Length;
        gridHeight = gridLines.Length;
        grid = new int[gridWidth, gridHeight];
        parseGrid(gridLines);
    }

    private void parseGrid(string[] lines)
    {
        for (int col = 0; col < gridWidth; col++)
        {
            for (int row = 0; row < gridHeight; row++)
            {
                grid[col, row] = int.Parse(lines[row][col].ToString());
            }
        }
    }

    public Cell GetCell(int col, int row) {
        return new Cell(grid[col,row], row,col);
    }

    public IEnumerable<int> WalkWest(Cell tree)
    {
        return Walk(tree, (-1,0));
    }

    public IEnumerable<int> WalkEast(Cell tree)
    {
        return Walk(tree, (1,0));
    }

    public IEnumerable<int> WalkNorth(Cell tree)
    {
        return Walk(tree, (0,1));
    }

    public IEnumerable<int> WalkSouth(Cell tree)
    {
        return Walk(tree, (0,-1));
    }

    public IEnumerable<int> Walk(Cell tree, (int dr, int dc) delta) {
        var row = tree.row + delta.dr;
        var col = tree.col + delta.dc;

        while ((0 <= row && row < this.gridHeight) && (0 <= col && col < this.gridWidth)) {
            yield return grid[col,row];
            row += delta.dr;
            col += delta.dc;
        } 
    }
}