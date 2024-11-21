using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    private Grid grid;

    private void Start()
    {
        ImprovedGrid improvedGrid = FindAnyObjectByType<ImprovedGrid>();
        grid = improvedGrid.grid;

        SetUpButtons();
    }

    private void SetUpButtons()
    {
        GameObject[] buttonObjects = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject buttonObject in buttonObjects)
        {
            Button button = buttonObject.GetComponent<Button>();
            if (button != null)
            {
                string buttonName = buttonObject.name;
                if (buttonName.StartsWith("SpawnButton "))
                {
                    if (int.TryParse(buttonName.Substring("SpawnButton".Length), out int columnIndex))
                    {
                        button.onClick.AddListener(() => SpawnCoinInColumn(columnIndex));
                    }
                }
            }
        }
    }
    
    private void SpawnCoinInColumn(int column)
    {
        Tile lowestTile = FindLowestEmptyTile(column);

        if (lowestTile != null)
        {
            GameObject newCoin = Instantiate(coinPrefab, lowestTile.Position + new Vector2(Tile.height/2f, Tile.width/2f), Quaternion.identity);

            grid.AddChildToTile(newCoin, column, grid.ConvertWorldToGridPos(lowestTile.Position.x, lowestTile.Position.y).gridPosY);

            Debug.Log($"Coin spawned at Tile[{column}, {grid.ConvertWorldToGridPos(lowestTile.Position.x, lowestTile.Position.y).gridPosY}]");
        }
        else
        {
            Debug.LogWarning($"Column {column} is full!");
        }
    }

    private Tile FindLowestEmptyTile(int column)
    {
        for (int y = 0; y < grid.tiles.GetLength(1); y++)
        {
            Tile tile = grid.GetTile(column, y);
            if (!tile.isOccupied)
            {
                return tile;
            }
        }
        return null;
    }
}
