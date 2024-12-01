using System.Collections.Generic;
using UnityEngine;

public class ImprovedGrid : MonoBehaviour
{
    public Grid grid;
    public GameObject GameWidthObj;
    private bool performCoroutine = true;
    [SerializeField] private string targetTag = "Coin";
    public GameObject canvas;

    private void Awake()
    {
        CreateGrid("First Grid Awake");
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
        if (grid == null) CreateGrid("First Grid with Checkgrid");
    }

    public void CreateGrid(string gridName)
    {
        var (width, height, startX, startY, canvas) = CheckGridSize();
        grid = new Grid(width, height, startX, startY, canvas, gridName);
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
    }

    private (int width, int height, float startX, float startY, GameObject canvas) CheckGridSize()
    {
        Renderer renderer = GameWidthObj.GetComponent<Renderer>();
        float gamewidth = renderer.bounds.size.x;
        float gameheight = renderer.bounds.size.y;

        int gridWidth = Mathf.FloorToInt(gamewidth / Tile.width);
        int gridHeight = Mathf.FloorToInt(gameheight / Tile.height);

        float startX = renderer.bounds.min.x;
        float startY = renderer.bounds.min.y;

        return (gridWidth, gridHeight, startX, startY, canvas);
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
    
    public void DeleteGrid()
    {
        StopAllCoroutines();

        if (grid.childToTileMap != null)
        {
            grid.childToTileMap.Clear();
        }

        if (grid._allChildren != null)
        {
            foreach (var child in grid._allChildren)
            {
                if (child != null)
                {
                    Destroy(child);
                }
            }
            grid._allChildren.Clear();
        }

        if (grid.tiles != null)
        {
            for (int x = 0; x < grid.tiles.GetLength(0); x++)
            {
                for (int y = 0; y < grid.tiles.GetLength(1); y++)
                {
                    grid.tiles[x, y] = null;
                }
            }
        }

        GameObject tileTextParent = GameObject.Find("TileTextParent");
        if (tileTextParent != null)
        {
            Destroy(tileTextParent);
        }

        grid = null;
    }
}