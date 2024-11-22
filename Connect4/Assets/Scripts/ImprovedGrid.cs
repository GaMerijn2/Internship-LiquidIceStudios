using System.Collections.Generic;
using UnityEngine;

public class ImprovedGrid : MonoBehaviour
{
    public Grid grid;
    public GameObject GameWidthObj;
    [SerializeField] private GameObject newGameObjectToAdd;
    public bool performCoroutine = true;
    [SerializeField] private string targetTag = "Coin";
    public GameObject canvas;


    private void Start()
    {
        CreateGrid();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateGrid();
        }
        CheckGrid();
        AddChildrenToList();

        if (performCoroutine)
        {
            StartCoroutine(grid.PerformFrameDelay(5));
            performCoroutine = !performCoroutine;
        }
    }

    private void UpdateGrid()
    {
        AddChildrenToList();
    }

    private void CheckGrid()
    {
        if (grid == null) CreateGrid();
    }

    public void CreateGrid()
    {
        var (width, height, startX, startY, canvas) = CheckGridSize();
        grid = new Grid(width, height, startX, startY, canvas);
    }

    private void AddChildrenToList()
    {
        List<GameObject> gameObjectsWithTag = new List<GameObject>();

        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in allGameObjects)
        {
            if (gameObject.CompareTag(targetTag))
            {
                gameObjectsWithTag.Add(gameObject);
            }
        }

        foreach (GameObject gameObject in gameObjectsWithTag)
        {
            if (grid.childToTileMap.ContainsKey(gameObject))
            {
                return;
            }
            grid.AddGameObject(gameObject);
        } 
        //Debug.Log($"Added {gameObjectsWithTag.Count} GameObjects with tag '{targetTag}' to the grid.");
        
        //add so the grid doesn't continuously add every child to the grid again, make it check if its already in the grid, and then dont add it
        
    }

    private (int width, int height, float startX, float startY, GameObject canvas) CheckGridSize()
    {
        Renderer renderer = GameWidthObj.GetComponent<Renderer>();
        float gamewidth = renderer.bounds.size.x;
        float gameheight = renderer.bounds.size.y;

        int gridWidth = Mathf.FloorToInt(gamewidth / Tile.width);
        int gridHeight = Mathf.FloorToInt(gameheight / Tile.height);

        float startX = renderer.bounds.min.x /*+ Tile.width / 2f*/;
        float startY = renderer.bounds.min.y /*- Tile.height / 2f*/;
        Debug.Log($"StartX: {startX}");
        Debug.Log($"StartY: {startY}");

        return (gridWidth, gridHeight, startX, startY, canvas);
    }

    public void RemoveGrid()
    {
        StopAllCoroutines();
        GameObject TileTextParent = GameObject.Find("TileTextParent");
        Destroy(TileTextParent);
        CreateGrid();
    }

    public void LogAllChildren()
    {
        Debug.Log($"Total number of objects in _allChildren: {grid._allChildren.Count}");
        foreach (var child in grid._allChildren)
        {
            Debug.Log($"Child Name: {child.name}");
        }
    }

    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            grid.DrawGrid();
        }
    }
}