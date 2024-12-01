using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class TweenObject
    {
        public Tween CoinMoveTween(GameObject target, Vector3 startVariable, Vector3 endVariable, float duration = 1f, Ease ease = Ease.Linear)
        {
            target.transform.position = startVariable;
            return target.transform.DOMove(endVariable, duration).SetEase(ease);
        }

    }
}
