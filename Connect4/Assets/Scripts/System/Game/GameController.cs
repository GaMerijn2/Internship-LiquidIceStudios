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
    
    private void Awake()
    {
        GameAssets.Initialize(redCoinPrefab, yellowCoinPrefab, placeSound, errorSound);
    }

    private void Start()
    {
        Grid grid = FindAnyObjectByType<ImprovedGrid>().grid;

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
        Tile lowestTile = _gridExtensions.GetLowestEmptyTile(column);

        if (lowestTile != null)
        {
            GameObject coin = _coinFactory.CreateCoinAtTile(lowestTile, column);
            _tweenObject.CoinMoveTween(coin, new Vector3(coin.transform.position.x, 5, coin.transform.position.z),lowestTile.Position + new Vector2(Tile.height / 2f, Tile.width / 2f), 1f);
            _soundPlayer.PlayPlacementSound();
            _teamCoordinator.SwitchTeam();
        }
        else
        {
            _soundPlayer.PlayErrorSound();
            Debug.LogWarning($"Column {column} is full!");
        }
    }
}