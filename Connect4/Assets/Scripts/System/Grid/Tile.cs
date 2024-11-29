using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public List<GameObject> children;
    public static int width = 1;
    public static int height = 1;
    public bool isOccupied = false;

    
    public Tile(Vector2 position)
    {
        Position = position;
        children = new List<GameObject>();
    }

    public void AddChild(GameObject child)
    {
        if (children.Contains(child)) return;
        children.Add(child);
    }

    public void RemoveChild(GameObject child)
    {
        if (!children.Contains(child)) return;
        children.Remove(child);
    }
    
    public Vector2 Position {  get; set;  }
}