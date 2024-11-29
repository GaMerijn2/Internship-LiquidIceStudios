using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class TweenObject : MonoBehaviour
{
    public void CoinMoveTween(GameObject target, Vector3 startVariable, Vector3 endVariable, float duration = 1f)
    {
        target.transform.position = startVariable;
        target.transform.DOMove(endVariable, duration);
    }
}
