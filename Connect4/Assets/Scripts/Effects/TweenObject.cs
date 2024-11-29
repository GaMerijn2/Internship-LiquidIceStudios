using DG.Tweening;
using UnityEngine;

public class TweenObject
{
    public void CoinMoveTween(GameObject target, Vector3 startVariable, Vector3 endVariable, float duration = 1f)
    {
        target.transform.position = startVariable;
        target.transform.DOMove(endVariable, duration);
    }
}
