using System.Collections.Generic;
using DG.Tweening;
using Effects;
using UnityEngine;

public class CoinFactory
{
    private  Grid _grid;
    private readonly TeamCoordinator _teamCoordinator;

    private List<GameObject> _coins;

    public void SetGridReference(Grid newGrid)
    {
        _grid = newGrid;
    }
    public CoinFactory(Grid grid, TeamCoordinator teamCoordinator)
    {
        _grid = grid;
        _teamCoordinator = teamCoordinator;
        _coins = new List<GameObject>();
    }

    public GameObject CreateCoinAtTile(Tile tile, int column)
    {
        GameObject newCoin = Object.Instantiate(
            _teamCoordinator.GetActiveCoinPrefab(),
            tile.Position + new Vector2(Tile.height / 2f, Tile.width / 2f),
            Quaternion.identity
        );
        _coins.Add(newCoin);

        _grid.AddChildToTile(newCoin, column, _grid.ConvertWorldToGridPos(tile.Position.x, tile.Position.y).gridPosY);
        return newCoin;
        
    }

    public void DeleteAllCoins(TweenObject _tweenObject)
    {
        foreach (var coin in _coins)
        {
            var duration = Random.Range(0.5f, 1);
            Tween coinTween = _tweenObject.CoinMoveTween(coin, coin.transform.position, new Vector3(coin.transform.position.x, -4.5f, coin.transform.position.z), duration, Ease.OutBounce);
            coinTween.OnKill(() => GameObject.Destroy(coin));
        }
        _coins.Clear();
    }


}