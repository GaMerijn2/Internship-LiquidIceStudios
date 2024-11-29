using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RowCheck : MonoBehaviour
{
    public Grid grid;
    public ImprovedGrid improvedGrid;
    private int teamId;
    public int amountForWin = 4;
    private GameObject _currentTeamObject;
    private bool _hasWon;
    [SerializeField] private ParticleSystem[] confetti;
    [SerializeField] private UnityEvent onFourOnARow;
    
    private void Start()
    {
        InitializeGrid();
    }
    private void Update()
    {
        Win();
    }

    private void Win()
    {
        if (_hasWon) return;
        bool winCheck = CheckForWin(amountForWin);
        if (!winCheck) return;
        _hasWon = true;
        onFourOnARow.Invoke();
        Debug.Log($"You win! {_currentTeamObject.GetComponent<TeamInfo>().teamName}");
        foreach (var confettiObj in confetti)
        {
            confettiObj.Play();
        }
    }

    private void InitializeGrid()
    {
        if (improvedGrid == null)
            improvedGrid = FindAnyObjectByType<ImprovedGrid>();
        if (improvedGrid != null && grid == null)
            grid = improvedGrid.grid;
    }
    private bool CheckForWin(int win)
    {
        InitializeGrid();
        if (grid == null) return false;
        for (int x = 0; x < grid.tiles.GetLength(0); x++)
        {
            for (int y = 0; y < grid.tiles.GetLength(1); y++)
            {
                Tile currentTile = grid.tiles[x, y];
                if (!currentTile.isOccupied) continue;
                teamId = currentTile.children[0].GetComponent<TeamInfo>().teamID;
                _currentTeamObject = currentTile.children[0];

                Vector2Int[] directions = {
                    new Vector2Int(1, 0),
                    new Vector2Int(1, 1),
                    new Vector2Int(0, 1),
                    new Vector2Int(1, -1)
                };

                foreach (var direction in directions)
                {
                    int count = 1;
                    for (int step = 1; step < win; step++)
                    {
                        int newX = x + direction.x * step;
                        int newY = y + direction.y * step;
                        if (newX < 0 || newX >= grid.tiles.GetLength(0) || newY < 0 || newY >= grid.tiles.GetLength(1)) break;
                        Tile nextTile = grid.tiles[newX, newY];
                        if (nextTile.isOccupied && nextTile.children[0].GetComponent<TeamInfo>().teamID == teamId) count++;
                        else break;
                        if (count == win)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

}
