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
    }

    public Tile GetLowestEmptyTile(int column)
    {
        if (_grid.tiles == null || _grid == null)
        {
            Debug.LogError("Grid has no tiles, or is Null");
        }
        for (int y = 0; y < _grid.tiles.GetLength(1); y++) // This gets the length of the column
        {
            Tile tile = _grid.GetTile(column, y); // Then it gets the lowest tile with the function _grid.GetTile(float worldPosX, float worldPosY)
            if (!tile.isOccupied) // And then it checks if there's a coin occupying
            {
                return tile; // If it is not occupied, it returns the lowest tile
            }
        }
        return null; // If there is no lowest tile, for example: The row is full, it returns nothing
    }
}
