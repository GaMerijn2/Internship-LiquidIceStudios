using DG.Tweening;
using Effects;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject redCoinPrefab;
    [SerializeField] private GameObject yellowCoinPrefab;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip errorSound;

    private TeamCoordinator _teamCoordinator;
    private SpawnButtonInitializer _buttonInitializer;
    private CoinFactory _coinFactory;
    private SoundPlayer _soundPlayer;
    private GridExtensions _gridExtensions;
    private TweenObject _tweenObject;
    private ImprovedGrid _improvedGrid;
    private Grid grid;
    public string gridName;
    
    [SerializeField] private int gridAmount = 0;
    
    private void Awake()
    {
        GameAssets.Initialize(redCoinPrefab, yellowCoinPrefab, placeSound, errorSound);
    }

    private void Start()
    {
        ResetReferences();
        gridName = grid.gridName;
    }

    private void ResetReferences()
    {
        _improvedGrid = null;
        grid = null;
        _soundPlayer = null;
        _teamCoordinator = null;
        _buttonInitializer = null;
        _gridExtensions = null;
        _coinFactory = null;
        _tweenObject = null;

        
        _improvedGrid = FindAnyObjectByType<ImprovedGrid>();
        grid = _improvedGrid.grid;

        _soundPlayer = new SoundPlayer(GetComponent<SoundExtensions>());
        _teamCoordinator = new TeamCoordinator();
        _buttonInitializer = new SpawnButtonInitializer(SpawnCoinInColumn);
        _gridExtensions = new GridExtensions(grid);
        _coinFactory = new CoinFactory(grid, _teamCoordinator);
        _tweenObject = new TweenObject();

        _buttonInitializer.InitializeButtons();
        _teamCoordinator.StartWithTeam(1);
    }

    private void SpawnCoinInColumn(int column)
    {
        if (grid == null || grid.tiles == null)
        {
            Debug.LogError("Grid has no tiles, or is Null");
            return;
        }
        Tile lowestTile = _gridExtensions.GetLowestEmptyTile(column);

        if (lowestTile != null)
        {
            GameObject coin = _coinFactory.CreateCoinAtTile(lowestTile, column);
            _tweenObject.CoinMoveTween(coin, new Vector3(coin.transform.position.x, 5, coin.transform.position.z),lowestTile.Position + new Vector2(Tile.height / 2f, Tile.width / 2f), 1f, Ease.OutBounce);
            _soundPlayer.PlayPlacementSound();
            _teamCoordinator.SwitchTeam();
        }
        else
        {
            _soundPlayer.PlayErrorSound();
            Debug.LogWarning($"Column {column} is full!");
        }
    }

    public void ResetGame()
    {
        _coinFactory.DeleteAllCoins(_tweenObject);
        _improvedGrid.DeleteGrid();
    
        int newGridAmount = gridAmount + 1;
        _improvedGrid.CreateGrid($"New Reset Grid {newGridAmount}");
        gridName = $"New Reset Grid {newGridAmount}";
    
        RowCheck rowCheck = FindObjectOfType<RowCheck>();
        if (rowCheck) rowCheck.SetGridReference(_improvedGrid.grid);

        _gridExtensions?.SetGridReference(_improvedGrid.grid);
        _coinFactory?.SetGridReference(_improvedGrid.grid);

        rowCheck._hasWon = false;
        gridAmount = newGridAmount;
    }
}