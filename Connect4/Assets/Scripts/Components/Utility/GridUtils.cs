using UnityEngine;

public class GridUtils
{
    public static void SnapToTileCenter(GameObject targetGameObject, Tile targetTile)
    {
        if (targetGameObject == null || targetTile == null)
        {
            Debug.LogError("Target GameObject or Tile is null");
            return;
        }

        Vector3 tileCenter = new Vector3(targetTile.Position.x + Tile.width / 2f, targetTile.Position.y + Tile.height / 2f, targetGameObject.transform.position.z);
        Debug.Log("Object was snapped to tile center");
        targetGameObject.transform.position = tileCenter;
    }
}