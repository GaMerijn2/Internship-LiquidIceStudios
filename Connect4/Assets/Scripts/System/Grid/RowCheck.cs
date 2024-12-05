using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RowCheck : MonoBehaviour
{
    public Grid grid;
    public ImprovedGrid improvedGrid;
    private int teamId;
    public int amountForWin = 4;
    private GameObject _currentTeamObject;
    public bool _hasWon;
    [SerializeField] private ParticleSystem[] confetti;
    [SerializeField] private UnityEvent onFourOnARow;
    [SerializeField] private TextMeshProUGUI winText;
    
    private void Start()
    {
        InitializeGrid();
    }
    private void Update()
    {
        CheckWin();
    }
    
    public void SetGridReference(Grid newGrid)
    {
        grid = newGrid;
    }

    public void ResetGridReference()
    {
        if (improvedGrid == null) improvedGrid = FindObjectOfType<ImprovedGrid>();
        if (improvedGrid != null && grid == null)
            grid = improvedGrid.grid;

        if (grid == null) 
        {
            Debug.LogError("Grid is null after reset.");
        }
    }

    public void SetAmountForWin(int newAmountForWin)
    {
        amountForWin = newAmountForWin;
    }

    private void CheckWin()
    {
        if (_hasWon) return;
        bool winCheck = CheckForWin(amountForWin);
        if (!winCheck) return;
        _hasWon = true;
        onFourOnARow.Invoke();
        winText.text = $"You win! \n {_currentTeamObject.GetComponent<TeamInfo>().teamName}";
        foreach (var confettiObj in confetti)
        {
            confettiObj.Play();
        }
    }

    public void InitializeGrid()
    {
        if (!improvedGrid)
            improvedGrid = FindAnyObjectByType<ImprovedGrid>();
        if (improvedGrid && grid == null)
            grid = improvedGrid.grid;
    }
    private bool CheckForWin(int win)
    {
        InitializeGrid();
        if (grid?.tiles == null)
        {
            Debug.LogError("Grid or Grid.tiles is null. Cannot check for win.");
            return false;
        }

        for (int x = 0; x < grid.tiles.GetLength(0); x++) // These for-loops loop over the whole grid, to check for every tile and its ajacent tiles
        {
            for (int y = 0; y < grid.tiles.GetLength(1); y++) // These for-loops loop over the whole grid, to check for every tile and its ajacent tiles
            {
                Tile currentTile = grid.tiles[x, y]; // This gets the tile that is currently geing looped over by the grid and stores it in the variable Tile

                                                                                // I am reviewing this right now, and i thing this if statement is not needed. This basicly doesn't do anything.
                if (currentTile == null || !currentTile.isOccupied) continue;   // It checks if the tile is nothing, or if the tile is not occupied and then it still continues.
                                                                                // This line can be removed to make the game perform better. 

                teamId = currentTile.children[0].GetComponent<TeamInfo>().teamID; // gets the teamId of the child that is in the 
                _currentTeamObject = currentTile.children[0];

                Vector2Int[] directions = {
                    new Vector2Int(1, 0), // This one goes right
                    new Vector2Int(1, 1), // This one goes from bottom left to top right
                    new Vector2Int(0, 1), // This one goes up
                    new Vector2Int(1, -1) // This one goes from top left to bottom right
                };

                foreach (var direction in directions) // loops over every direction to find the tile next to it
                {
                    int count = 1; // This saves the amount of coins that are next to each other, and ill be from the same team
                    for (int step = 1; step < win; step++) // loops over the amount you need to win (win = 4 by default).  
                    {
                        int newX = x + direction.x * step; // calculates the new tile that is next to it for the x axis
                        int newY = y + direction.y * step; // same as abov this one, but the y axis

                        if (newX < 0 || newX >= grid.tiles.GetLength(0) || newY < 0 || newY >= grid.tiles.GetLength(1)) break; // checks if the tile will still be in the grid, otherwise stops the for-loop

                        Tile nextTile = grid.tiles[newX, newY]; // gets the new tile with the newX and newY
                        if (nextTile != null && nextTile.isOccupied && nextTile.children[0].GetComponent<TeamInfo>().teamID == teamId) // if the coin in the tile next to it is the same team, adds one to the win counter
                            count++;
                        else
                            break;

                        if (count == win)
                        {
                            return true; // you have won, returns true to the win function variable this is being used at
                        }
                    }
                }
            }
        }
        return false; // returns false ofcourse
    }


}
