using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject currentCoinPrefab;
    [SerializeField] private GameObject redCoinPrefab;
    [SerializeField] private GameObject yellowCoinPrefab;
    private SoundExtensions _soundExtensions;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip errorSound;
    public Grid grid;

    public int team = 1;

    private void Start()
    {
        ImprovedGrid improvedGrid = FindAnyObjectByType<ImprovedGrid>();
        grid = improvedGrid.grid;
        _soundExtensions = GetComponent<SoundExtensions>();

        SetUpButtons();
        SetTeam();
    }

    private void SetTeam()
    {
        switch (team)
        {
            case 1:
                currentCoinPrefab = redCoinPrefab;
                currentCoinPrefab.GetComponent<TeamInfo>().teamID = team;
                currentCoinPrefab.GetComponent<TeamInfo>().teamName = "Peach";
                break;
            case 2:
                currentCoinPrefab = yellowCoinPrefab;
                currentCoinPrefab.GetComponent<TeamInfo>().teamID = team;
                currentCoinPrefab.GetComponent<TeamInfo>().teamName = "Teal";
                break;
            default:
                currentCoinPrefab = redCoinPrefab;
                break;
        }
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
            GameObject newCoin = Instantiate(currentCoinPrefab, lowestTile.Position + new Vector2(Tile.height/2f, Tile.width/2f), Quaternion.identity);
            _soundExtensions.PlaySoundFile(placeSound);
            grid.AddChildToTile(newCoin, column, grid.ConvertWorldToGridPos(lowestTile.Position.x, lowestTile.Position.y).gridPosY);
            grid._allChildren.Add(newCoin);
            //Debug.Log($"Coin spawned at Tile[{column}, {grid.ConvertWorldToGridPos(lowestTile.Position.x, lowestTile.Position.y).gridPosY}]");
            team = team == 1 ? 2 : 1;
            SetTeam();
        }
        else
        {
            _soundExtensions.PlaySoundFile(errorSound, 0.4f);
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
