using DG.Tweening;
using UnityEngine;

namespace Effects
{
    public class TweenObject
    {
        public void CoinMoveTween(GameObject target, Vector3 startVariable, Vector3 endVariable, float duration = 1f)
        {
            target.transform.position = startVariable;
            target.transform.DOMove(endVariable, duration).SetEase(EaseOutBounceCustom);
        }

        private float EaseOutBounceCustom(float time, float duration, float overshootOrAmplitude, float period)
        {
            float x = time / duration;
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (x < 1 / d1)
            {
                return n1 * x * x;
            } else if (x < 2 / d1)
            {
                x -= 1.5f / d1;
                return n1 * x * x + 0.75f;
            } else if (x < 2.5f / d1)
            {
                x -= 2.25f / d1;
                return n1 * x * x + 0.9375f;
            } else
            {
                x -= 2.625f / d1;
                return n1 * x * x + 0.984375f;
            }
        }

    }
}
