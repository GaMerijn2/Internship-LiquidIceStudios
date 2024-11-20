using UnityEngine;

public class GridUtils
{
    // Method to snap the GameObject to the center of the specified tile
    public static void SnapToTileCenter(GameObject targetGameObject, Tile targetTile)
    {
        if (targetGameObject == null || targetTile == null)
        {
            Debug.LogError("Target GameObject or Tile is null");
            return;
        }

        // Calculate the center of the tile
        Vector3 tileCenter = new Vector3(targetTile.Position.x + Tile.width / 2f, targetTile.Position.y + Tile.height / 2f, targetGameObject.transform.position.z);

        // Set the GameObject's position to the tile's center
        targetGameObject.transform.position = tileCenter;
    }
}