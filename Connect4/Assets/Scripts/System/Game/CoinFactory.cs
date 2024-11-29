using UnityEngine;

public class CoinFactory
{
    private readonly Grid _grid;
    private readonly TeamCoordinator _teamCoordinator;

    public CoinFactory(Grid grid, TeamCoordinator teamCoordinator)
    {
        _grid = grid;
        _teamCoordinator = teamCoordinator;
    }

    public void CreateCoinAtTile(Tile tile, int column)
    {
        GameObject newCoin = Object.Instantiate(
            _teamCoordinator.GetActiveCoinPrefab(),
            tile.Position + new Vector2(Tile.height / 2f, Tile.width / 2f),
            Quaternion.identity
        );

        _grid.AddChildToTile(newCoin, column, _grid.ConvertWorldToGridPos(tile.Position.x, tile.Position.y).gridPosY);
    }
}