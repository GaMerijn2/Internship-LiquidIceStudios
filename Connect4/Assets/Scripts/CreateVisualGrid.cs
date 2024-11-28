
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CreateVisualGrid : MonoBehaviour
{
    private Grid _grid;
    public ImprovedGrid improvedGrid;
    private GameObject visual;
    private Tile[,] tiles;
    private GameObject parent;

    [SerializeField] private GameObject gridVisuals;
    [SerializeField] private GameObject gridVisualsParent;

    private void Start()
    {
        InitializeGrid();
        tiles = _grid.tiles;
        parent = new GameObject("Parent");
        parent.transform.parent = gridVisualsParent.transform;
        StartCoroutine(setVisual());
    }

    private IEnumerator setVisual()
    {
        foreach (var tile in tiles)
        {
            visual = Instantiate(gridVisuals, new Vector3
                (tile.Position.x + Tile.width / 2f,
                    tile.Position.y + Tile.height / 2f,
                    0),
                Quaternion.identity,
                parent.transform);
            StartCoroutine(SetScaleTween(0,0.05f));
            yield return new WaitForSeconds(.1f);
        }
    }

    private IEnumerator SetScaleTween(float delay = 0.1f, float duration = 1f)
    {
        visual.transform.localScale = new Vector3(0, 0, 0);
        visual.transform.DOScale(1.1f, duration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                visual.transform.DOScale(1f, duration / 2)
                    .SetEase(Ease.InQuad);
            });
        yield return new WaitForSeconds(delay);
    }
    
    private void InitializeGrid()
    {
        if (improvedGrid == null)
            improvedGrid = FindAnyObjectByType<ImprovedGrid>();
        if (improvedGrid != null && _grid == null)
            _grid = improvedGrid.grid;
    }
}
