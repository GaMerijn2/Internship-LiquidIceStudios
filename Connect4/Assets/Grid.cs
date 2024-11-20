using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Grid
{
    public Dictionary<GameObject, Tile> childToTileMap = new Dictionary<GameObject, Tile>();
    public Tile[,] tiles;
    
    private int width;
    private int height;
    
    public List<GameObject> _allChildren;

    private GameObject GameWidthObject;

    
    private List<Tile> occupiedTiles = new List<Tile>(); // todo: in de toekomt misschien een aparrte rendergrid class voor maken

    //public GameObject tileText = new GameObject();
    
    public Grid(int width, int height, float startX = 0, float startY=0, GameObject canvas=null)
    {
        this.width = width;
        this.height = height;
        Position = new Vector2(startX, startY);
        _allChildren = new List<GameObject>();
        CreateTiles(width, height, startX, startY, canvas);
    }

    private void CreateTiles(int width, int height, float startX, float startY,GameObject canvas)
    {
        GameWidthObject = canvas;
        tiles = new Tile[width, height];
        GameObject TileTextParent = new GameObject("TileTextParent");
        TileTextParent.transform.SetParent(canvas.transform);
    
        Debug.Log($"{startX}, {startY}");
        GameObject startPositionVisualizer = new GameObject("StartPositionVisualizer");
        startPositionVisualizer.transform.position = new Vector3(startX + Tile.width/2f, startY+Tile.height/2f, 0);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var position = new Vector2(startX + x * Tile.width, startY + y * Tile.height);
                tiles[x, y] = new Tile(position);

                var tileText = new GameObject("TileText");
                tileText.transform.SetParent(TileTextParent.transform);

                RectTransform rectTransform = tileText.AddComponent<RectTransform>();
                rectTransform.anchoredPosition = position += new Vector2(Tile.width/2f, Tile.height/2f);
                rectTransform.sizeDelta = new Vector2(Tile.width, Tile.height);

                TextMeshProUGUI tileTextMesh = tileText.GetOrAddComponent<TextMeshProUGUI>();
                tileTextMesh.text = $"Tile[{x},{y}]";
                tileTextMesh.font = Resources.Load<TMP_FontAsset>("ethnocentric.rg-regular SDF");
                tileTextMesh.fontSize = Tile.width / 4f;
            }
        }
    }


    public IEnumerator PerformFrameDelay(int frameInterval)
    {
        var waitTime = 1 / 60 * frameInterval;
        int currentGridX = 0;
        int currentGridY = 0;
        
        while (true)
        {
            occupiedTiles.Clear();
            foreach (var currentChild in _allChildren)
            {
                if(currentChild is null) _allChildren.Remove(currentChild);
                var currentChildTile = childToTileMap.ContainsKey(currentChild) ? childToTileMap[currentChild] : null;
                Vector3 childPos = currentChild.transform.position;
                Vector3 gridPos = Position;
                Vector3 diff = childPos - gridPos;
                float tileX = diff.x / Tile.width;
                float tileY = diff.y / Tile.height;
                int gridX = Mathf.FloorToInt(tileX);
                int gridY = Mathf.FloorToInt(tileY);
                //Debug.Log($"Grid X: {gridX}, Y: {gridY}");
                Tile correctTile = tiles[gridX, gridY];
                
                if (currentChildTile != null)
                {
                    currentGridX = Mathf.FloorToInt(currentChildTile.Position.x);
                    currentGridY = Mathf.FloorToInt(currentChildTile.Position.y);
                }
                
                if (currentChildTile == correctTile)
                {
                    occupiedTiles.Add(correctTile);
                    GridUtils.SnapToTileCenter(currentChild, correctTile);

                    continue;
                }
                
                if (currentChildTile == null)
                {
                    AddChildToTile(currentChild, gridX, gridY);
                }
                
                if (currentChild != null && currentChildTile != correctTile)
                {
                    RemoveChildFromTile(currentChild, currentGridX, currentGridY);
                    AddChildToTile(currentChild, gridX, gridY);
                }
            }
            yield return new WaitForSeconds(waitTime);
            
        }
    }
    public void AddGameObject(GameObject targetGameObject)
    {
        if (_allChildren.Contains(targetGameObject)) return;
        _allChildren.Add(targetGameObject);
    }
    public void RemoveGameObject(GameObject targetGameObject)                                                                        
    {                                                                                                                             
        _allChildren.Remove(targetGameObject);                                                                                       
    }
    public void AddChildToTile(GameObject child, int x, int y)
    {
        if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1)) return;
        
        var currentTile = tiles[x, y];
        currentTile.AddChild(child);
        childToTileMap[child] = currentTile;
        var currentGridInfo = child.GetOrAddComponent<GridInfo>();
        currentGridInfo.currentTile = currentTile;
    }
    public void RemoveChildFromTile(GameObject child, int x, int y)
    {
        if (x < 0 || x >= tiles.GetLength(0) || y < 0 || y >= tiles.GetLength(1)) return;
        
        tiles[x, y].RemoveChild(child);
    }
    
    public (int gridPosX, int gridPosY) ConvertWorldToGridPos(float worldPosX, float worldPosY)
    {
        var gameObjectWidth = GameObject.FindGameObjectWithTag("GameWidthObject");
        if (gameObjectWidth == null)
        {
            Debug.LogError("Game Object Width is null");
        }
        var renderer = gameObjectWidth.GetComponent<Renderer>();

        float startX = renderer.bounds.min.x;
        float startY = renderer.bounds.min.y;
        
        float adjustedWorldPosX = worldPosX - startX;
        float adjustedWorldPosY = worldPosY - startY;
        
        int gridPosX = Mathf.FloorToInt(adjustedWorldPosX / Tile.width);
        int gridPosY = Mathf.FloorToInt(adjustedWorldPosY / Tile.height);
        
        return (gridPosX, gridPosY);
    } 
    
    public Tile GetTile(GameObject targetGameObject)
    {
        return childToTileMap[targetGameObject];
    }

    public Tile GetTile(Vector2Int cellPosition)
    {
        return GetTile(cellPosition.x, cellPosition.y);
    }
    
    public Tile GetTile(int cellX, int cellY)
    {
        // voeg extra checks to om te kijken of deze x en y wel binnen het grid vallen
        // bounds check
        return tiles[cellX, cellY];
    }

    public Tile GetTile(float xWorldPos, float yWorldPos)
    {
        (int x, int y) xAndY = ConvertWorldToGridPos(xWorldPos, yWorldPos);
        return tiles[xAndY.x, xAndY.y];
    }
    
    public void DrawGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var currentTile = tiles[x, y];
                if (occupiedTiles.Contains(currentTile))
                {
                    Gizmos.color = Color.red;
                }                               
                else
                {
                    Gizmos.color = Color.gray;
                }
                Gizmos.DrawWireCube(new Vector3(currentTile.Position.x + Tile.width/2f, currentTile.Position.y + Tile.height/2f, 0), new Vector3(Tile.width/0.985f, Tile.height/0.985f, 0));
            }
        }
    }

    public int GridWidth
    {
        get => Tile.width * width;
    }
    public Vector2 Position {  get; set;  }
}