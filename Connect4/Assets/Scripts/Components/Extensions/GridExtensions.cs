using UnityEngine;

public class GridExtensions
{
    private  Grid _grid;

    public GridExtensions(Grid grid)
    {
        _grid = grid;
    }
    
    public void SetGridReference(Grid newGrid)
    {
        _grid = newGrid;
        Debug.Log($"GridExtensions grid updated to: {_grid.gridName}");
    }

    public Tile GetLowestEmptyTile(int column)
    {
        if (_grid.tiles == null || _grid == null)
        {
            Debug.LogError("Grid has no tiles, or is Null");
        }
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