public class GridExtensions
{
    private readonly Grid _grid;

    public GridExtensions(Grid grid)
    {
        _grid = grid;
    }

    public Tile GetLowestEmptyTile(int column)
    {
        for (int y = 0; y < _grid.tiles.GetLength(1); y++)
        {
            Tile tile = _grid.GetTile(column, y);
            if (!tile.isOccupied)
            {
                return tile;
            }
        }
        return null;
    }
}